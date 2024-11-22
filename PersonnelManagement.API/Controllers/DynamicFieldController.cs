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
    public class DynamicFieldController : ControllerBase
    {
        private readonly IFieldDefinitionService _FieldDefService;
        private readonly IMapper _mapper;
        public DynamicFieldController(IFieldDefinitionService FieldDefService, IMapper Mapper)
        {
            _FieldDefService = FieldDefService;       
            _mapper = Mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDynamicField ([FromBody] DynamicFieldModel FieldObj)
        {
            try
            {
                long resultId = 0;
                if (FieldObj == null)
                {
                    return BadRequest("ابجکت ورودی نال است");
                }
                NewFieldDTO newField = _mapper.Map<NewFieldDTO>(FieldObj);
                resultId = await _FieldDefService.CreateFieldAsync(newField);
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
