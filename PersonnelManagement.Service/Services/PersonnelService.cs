using AutoMapper;
using PersonnelManagement.Data.Entities;
using PersonnelManagement.Data.Repository.Contract;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IRepository<DynamicFieldDefinition> _RFieldDefinition;
        private readonly IRepository<PersonInfo> _RPersonInfo;
        private readonly IMapper _mapper;

        public PersonnelService(IMapper mapper, IRepository<DynamicFieldDefinition> RFieldDefinition,
            IRepository<PersonInfo> RPersonInfo)
        {
            _mapper = mapper;
            _RFieldDefinition = RFieldDefinition;
            _RPersonInfo = RPersonInfo;
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
    }
}
