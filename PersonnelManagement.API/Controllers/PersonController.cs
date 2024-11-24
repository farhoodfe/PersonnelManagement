using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.API.Models;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;

namespace PersonnelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelService _PersonnelService;
        public PersonController(IMapper mapper, IPersonnelService personnelService)
        {
            _mapper = mapper;
            _PersonnelService = personnelService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonInfoModel newPerson)
        {
            try
            {
                long resultId = 0;
                if (newPerson == null)
                {
                    return BadRequest("ابجکت ورودی نال است");
                }
                PersonInfoDTO person = new PersonInfoDTO();
                person.FName = newPerson.FName;
                person.LName = newPerson.LName;
                person.PersonnelCode = newPerson.PersonnelCode;

                List<SubmissionDTO> subs = new List<SubmissionDTO>();
                foreach (SubmissionModel Sub in newPerson.Submissions)
                {
                    subs.Add(new SubmissionDTO { Fk_FieldDefinition = Sub.FieldId, FieldValue=Sub.FieldValue });
                }
                person.Submissions = subs;

                resultId = await _PersonnelService.CreatePersonAsync(person);
                if (resultId > 0)
                    return Ok(resultId);
                else
                    return BadRequest("خطا");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
