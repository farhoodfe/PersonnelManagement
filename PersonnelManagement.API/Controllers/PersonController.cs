using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.API.Models;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using System.Net.WebSockets;

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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllPersonInfos()
        {
            try
            {
                IEnumerable<PersonInfoDTO> PersonsList;
                PersonsList = await _PersonnelService.GetAllPersonsAsync();

                

                return Ok(PersonsList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("{id:long}", Name = "GetPersonById")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            try
            {
                PersonInfoModel person = new PersonInfoModel();
                PersonInfoDTO p = new PersonInfoDTO();
                p = await _PersonnelService.GetPersonById(id);
                person.Id = p.Id;
                person.FName = p.FName;
                person.LName = p.LName;
                person.PersonnelCode = p.PersonnelCode;
                List<SubmissionModel> subs = new List<SubmissionModel>();
                if (p.Submissions!= null )
                    foreach (var sub in p.Submissions)
                    {
                        subs.Add(_mapper.Map<SubmissionModel>(sub));
                    }
                person.Submissions = subs;
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        public async Task<ActionResult> UpdatePerson(long id, [FromBody] PersonInfoModel updateDTO)
        {
            try
            {
                if (updateDTO == null )
                {
                    return BadRequest();
                }
                PersonInfoDTO person = new PersonInfoDTO();
                person.Id = id;
                person.FName=updateDTO.FName;
                person.LName=updateDTO.LName;
                person.PersonnelCode=updateDTO.PersonnelCode;
                person.Submissions = new List<SubmissionDTO>();
                if (updateDTO.Submissions!= null )
                {
                    foreach (var sub in updateDTO.Submissions)
                        person.Submissions.Add(_mapper.Map<SubmissionDTO>(sub));
                }
                await _PersonnelService.UpdatePerson(id,person);
                return Ok("شخص با موفقیت بروز گردید");
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.ToString() );
            }
        }
    }
}
