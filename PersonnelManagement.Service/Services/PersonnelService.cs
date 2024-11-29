using AutoMapper;
using PersonnelManagement.Data.Entities;
using PersonnelManagement.Data.Repository.Contract;
using PersonnelManagement.Data.Statics;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IRepository<DynamicFieldDefinition> _RFieldDefinition;
        private readonly IRepository<PersonInfo> _RPersonInfo;
        private readonly IRepository<FieldSubmission> _RFieldSubmission;
        private readonly IMapper _mapper;

        public PersonnelService(IMapper mapper, IRepository<DynamicFieldDefinition> RFieldDefinition,
            IRepository<PersonInfo> RPersonInfo, IRepository<FieldSubmission> RFieldSubmission)
        {
            _mapper = mapper;
            _RFieldDefinition = RFieldDefinition;
            _RPersonInfo = RPersonInfo;
            _RFieldSubmission = RFieldSubmission;
        }

        public async Task<long> CreatePersonAsync(PersonInfoDTO PersonModel)
        {
            if (PersonModel == null) { throw new ArgumentNullException(); }

            PersonInfo newPerson= new PersonInfo();
            newPerson.FName = PersonModel.FName;
            newPerson.LName = PersonModel.LName;
            newPerson.PersonnelCode = PersonModel.PersonnelCode;
            newPerson.IsDeleted = false;
           

            List<FieldSubmission> submissions = new List<FieldSubmission>();
            foreach (SubmissionDTO sub in PersonModel.Submissions)
            {
                FieldSubmission fs = new FieldSubmission();
                fs.Fk_FieldDefinition = sub.Fk_FieldDefinition;
                fs.FieldValue = sub.FieldValue;
                fs.IsDeleted = false;
                submissions.Add (fs);
            }
            newPerson.FieldSubmissions = submissions;
             await _RPersonInfo.CreateAsync(newPerson);
            return (newPerson.Id);

        }

        public async Task<ICollection<PersonInfoDTO>> GetAllPersonsAsync()
        {
            IEnumerable<PersonInfo> personList;
            personList = await _RPersonInfo.GetAllAsync(u => u.IsDeleted==false);
            
            List<PersonInfoDTO> result = new List<PersonInfoDTO> ();
            foreach (PersonInfo person in personList)
            {
                PersonInfoDTO p = new PersonInfoDTO();
                p.FName = person.FName;
                p.LName = person.LName;
                p.PersonnelCode = person.PersonnelCode;
                p.Submissions =await GetPersonSubmissions(person.Id);
                result.Add(p);
            }
            return result;
        }

        public async Task<PersonInfoDTO> GetPersonById(long Id)
        {
            if (Id>0)
            {
                PersonInfo person = await _RPersonInfo.FindAsync(Id);
                PersonInfoDTO resultPerson = new PersonInfoDTO();
                if (person != null)
                {
                    resultPerson.Id = person.Id;
                    resultPerson.FName = person.FName;
                    resultPerson.LName = person.LName;
                    resultPerson.PersonnelCode=person.PersonnelCode;
                    //Getting Submissions List
                    resultPerson.Submissions = await GetPersonSubmissions(person.Id);
                    return resultPerson;
                }
                
            }
            return null;
        }

        public async Task<ICollection<SubmissionDTO>> GetPersonSubmissions(long Id)
        {
            var person = _RPersonInfo.FindAsync(Id);
            if (person == null)
                return null;
            List<SubmissionDTO> result = new List<SubmissionDTO>();
            List<FieldSubmission> SubList = new List<FieldSubmission>();
            SubList = await _RFieldSubmission.GetAllAsync(u => u.Fk_PersonInfo == Id && (u.IsDeleted == null || u.IsDeleted==false));
            if (SubList.Count > 0)
            {
                SubmissionDTO sb = new SubmissionDTO();
                foreach (FieldSubmission sub in SubList)
                {
                    DynamicFieldDefinition f = new DynamicFieldDefinition();
                    f = await _RFieldDefinition.FindAsync(sub.Fk_FieldDefinition);
                    if (f.IsDeleted == false|| f.IsDeleted==null)
                    {
                        sb = _mapper.Map<SubmissionDTO>(sub);
                        sb.DisplayName = f.DisplayName;

                        result.Add(sb);
                    }                }

            }
            return result;
        }

        public async Task<PersonInfoDTO> UpdatePerson(long Id, PersonInfoDTO updateDTO)
        {
            PersonInfo person = await _RPersonInfo.FindAsync(Id);
            if (person != null)
            {
                person.Id = Id;
                person.FName = updateDTO.FName;
                person.LName = updateDTO.LName;
                person.PersonnelCode = updateDTO.PersonnelCode;
                person.FieldSubmissions = new List<FieldSubmission>();
                if (updateDTO.Submissions!= null)
                {
                    foreach(var sub in updateDTO.Submissions)
                    { 
                        FieldSubmission fs = new FieldSubmission();
                        fs = await _RFieldSubmission.GetAsync(u => u.Fk_FieldDefinition == sub.Fk_FieldDefinition
                                && (u.IsDeleted == false || u.IsDeleted == null));
                        fs.FieldValue = sub.FieldValue;
                        person.FieldSubmissions.Add(fs);
                    }
                }
                await _RPersonInfo.UpdateAsync(person);
                return updateDTO;
            }
            return null;


        }

        public async Task<bool> DeletePerson(long id)
        {
            try
            {
                PersonInfo person = new PersonInfo();
                person = await _RPersonInfo.FindAsync(id);
                if (person != null)
                {
                    person.IsDeleted = true;
                }
                await _RPersonInfo.UpdateAsync(person);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<PersonInfoDTO>> GetFilteredPersons(PersonInfoDTO personFilterDTO)
        {
            try
            {
                var filters = new List<Expression<Func<PersonInfo, bool>>>();
                var submissionFilters = new List<Expression<Func<FieldSubmission, bool>>>();

                // Static field filters
                if (!string.IsNullOrEmpty(personFilterDTO.FName))
                {
                    filters.Add(p => p.FName.Contains(personFilterDTO.FName));
                }

                if (!string.IsNullOrEmpty(personFilterDTO.LName))
                {
                    filters.Add(p => p.LName.Contains(personFilterDTO.LName));
                }

                if (!string.IsNullOrEmpty(personFilterDTO.PersonnelCode))
                {
                    filters.Add(p => p.PersonnelCode.Contains(personFilterDTO.PersonnelCode));
                }
               

                // Fetch filtered persons based on static fields
                List<PersonInfo> persons = (List<PersonInfo>)await _RPersonInfo.GetFilteredAsync(filters);
                persons = persons.Where(p => p.IsDeleted == false).ToList();
                foreach (PersonInfo person in persons)
                {
                    List<SubmissionDTO> subs = (List<SubmissionDTO>)await GetPersonSubmissions(person.Id);
                    foreach (SubmissionDTO submission in subs)
                        person.FieldSubmissions.Add(_mapper.Map<FieldSubmission>(submission));
                }
               
                //Dynamic Fields Filter
                var personIds = persons.Select(p => p.Id).ToList();

                foreach (var filter in personFilterDTO.Submissions)
                {
                    if (!string.IsNullOrEmpty(filter.FieldValue)) // Skip submissions without a value
                    {
                        submissionFilters.Add(fs =>
                            personIds.Contains(fs.Fk_PersonInfo) &&
                            fs.Fk_FieldDefinition == filter.Fk_FieldDefinition &&
                            (
                                ((int)fs.fieldDefinition.Type == 0 && fs.FieldValue.Contains(filter.FieldValue)) ||
                                ((int)fs.fieldDefinition.Type == 1 && fs.FieldValue.Contains(filter.FieldValue)) ||
                                ((int)fs.fieldDefinition.Type == 2 && fs.FieldValue == filter.FieldValue)
                            )
                        );
                    }
                }
                if (submissionFilters.Any())
                {
                    // Apply dynamic field filters only if there are any
                    var submissions = await _RFieldSubmission.GetFilteredAsync(submissionFilters);

                    // Refine persons based on submissions
                    var matchedPersonIds = submissions.Select(s => s.Fk_PersonInfo).Distinct().ToList();
                    persons = persons.Where(p => matchedPersonIds.Contains(p.Id)).ToList();
                }

                List<PersonInfoDTO> result = new List<PersonInfoDTO>();
                foreach (PersonInfo person in persons)
                {
                    PersonInfoDTO p = new PersonInfoDTO();
                    p.FName = person.FName;
                    p.LName = person.LName;
                    p.PersonnelCode = person.PersonnelCode;
                    p.Submissions = await GetPersonSubmissions(person.Id);
                    result.Add(p);
                }
                return result;
            }


            catch (Exception ex)
            {
                throw new Exception( ex.ToString());
            }
        }

    }
}
