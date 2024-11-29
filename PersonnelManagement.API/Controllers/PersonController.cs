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
        private readonly IFieldDefinitionService _FieldService;
        public PersonController(IMapper mapper, IPersonnelService personnelService, IFieldDefinitionService fieldDefinitionService)
        {
            _mapper = mapper;
            _PersonnelService = personnelService;
            _FieldService = fieldDefinitionService;
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

        [HttpPut("[action]")]
        public async Task<IActionResult> DeletePerson([FromQuery] int Id)
        {
            try
            {
                if (await _PersonnelService.DeletePerson(Id)) ;
                return Ok("شخص با موفقیت حذف شد");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFilteredPersons([FromBody] PersonInfoModel personFilter)
        {
            try
            {
                if (personFilter == null)
                {
                    return BadRequest();
                }
                PersonInfoDTO person = new PersonInfoDTO();
                person.FName = personFilter.FName;
                person.LName = personFilter.LName;
                person.PersonnelCode = personFilter.PersonnelCode;
                person.Submissions = new List<SubmissionDTO>();
                foreach (var sub in personFilter.Submissions)
                {
                    if (sub.FieldId>0)
                        person.Submissions.Add( _mapper.Map<SubmissionDTO>(sub));
                }
                //if (personFilter.Submissions != null)
                //{
                //    foreach (var sub in personFilter.Submissions)
                //        person.Submissions.Add(_mapper.Map<SubmissionDTO>(sub));
                //}

                //var allFields = (List<NewFieldDTO>)await _FieldService.GetAllFieldsAsync(0, 1);
                //var fieldIds = person.Submissions.Where(s => s.Fk_FieldDefinition>0).Select(s => s.Fk_FieldDefinition).ToList();
                //foreach (var field in allFields)
                //{
                //    if (!fieldIds.Contains(field.Id))
                //        person.Submissions.Add(new SubmissionDTO { Fk_FieldDefinition = field.Id, FieldValue = "", DisplayName = "" });
                //}
                

                List<PersonInfoDTO> personList = new List<PersonInfoDTO>();
                personList = (List<PersonInfoDTO>)await _PersonnelService.GetFilteredPersons(person);
                return Ok(personList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
