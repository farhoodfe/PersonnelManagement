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
        private readonly IRepository<PersonInfo> _RPersonIfno;
        private readonly IMapper _mapper;

        public PersonnelService(IMapper mapper, IRepository<DynamicFieldDefinition> RFieldDefinition,
            IRepository<PersonInfo> RPersonInfo)
        {
            _mapper = mapper;
            _RFieldDefinition = RFieldDefinition;
            _RPersonIfno = RPersonInfo;
        }
        public async Task<long> CreatePersonAsync(PersonInfoDTO newPerson)
        {
            if (newPerson == null) { throw new ArgumentNullException(); }

            PersonInfo person= new PersonInfo();
            person.FName = newPerson.FName;
            person.LName = newPerson.LName;
            person.PersonnelCode = newPerson.PersonnelCode;

            List<FieldSubmission> submissions = new List<FieldSubmission>();
            foreach (SubmissionDTO sub in newPerson.Submissions)
            {
                submissions.Add (_mapper.Map<FieldSubmission>(sub));
            }
            person.FieldSubmissions = submissions;
            await _RPersonIfno.CreateAsync(person);
            return (person.Id);

        }
    }
}
