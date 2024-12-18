﻿using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.Contracts
{
    public interface IPersonnelService
    {
        Task<long> CreatePersonAsync(PersonInfoDTO newPerson);
        Task<ICollection<PersonInfoDTO>> GetAllPersonsAsync();
        Task<PersonInfoDTO> GetPersonById(long Id);
        Task<PersonInfoDTO> UpdatePerson(long Id, PersonInfoDTO person);
        Task<ICollection<SubmissionDTO>> GetPersonSubmissions(long Id);
        Task<bool> DeletePerson(long id);
        Task<ICollection<PersonInfoDTO>> GetFilteredPersons(PersonInfoDTO person);
    }
}
