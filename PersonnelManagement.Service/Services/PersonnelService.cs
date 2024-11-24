using AutoMapper;
using PersonnelManagement.Data.Entities;
using PersonnelManagement.Data.Repository.Contract;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

           

            List<FieldSubmission> submissions = new List<FieldSubmission>();
            foreach (SubmissionDTO sub in PersonModel.Submissions)
            {
                FieldSubmission fs = new FieldSubmission();
                fs.Fk_FieldDefinition = sub.Fk_FieldDefinition;
                fs.FieldValue = sub.FieldValue;
                submissions.Add (fs);
            }
            newPerson.FieldSubmissions = submissions;
             await _RPersonInfo.CreateAsync(newPerson);
            return (newPerson.Id);

        }

        public async Task<ICollection<PersonInfoDTO>> GetAllPersonsAsync()
        {
            IEnumerable<PersonInfo> personList;
            personList = await _RPersonInfo.GetAllAsync();
            
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

        public async Task<ICollection<SubmissionDTO>> GetPersonSubmissions(long Id)
        {
            var person = _RPersonInfo.FindAsync(Id);
            if (person == null)
                return null;
            List<SubmissionDTO> result = new List<SubmissionDTO>();
            List<FieldSubmission> SubList = new List<FieldSubmission>();
            SubList = await _RFieldSubmission.GetAllAsync(u => u.Fk_PersonInfo == Id);
            if (SubList.Count > 0)
            {
                SubmissionDTO sb = new SubmissionDTO();
                foreach (FieldSubmission sub in SubList)
                {
                    DynamicFieldDefinition f = new DynamicFieldDefinition();
                    f = await _RFieldDefinition.FindAsync(sub.Fk_FieldDefinition);
                    sb.DisplayName = f.DisplayName;
                    sb =_mapper.Map<SubmissionDTO>(sub);
                    
                    result.Add(sb);
                }

            }
            return result;
        }
    }
}
