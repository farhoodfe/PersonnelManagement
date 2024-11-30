using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.API.Models;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using PersonnelManagement.Service.Services;

namespace PersonnelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private readonly IFormulaService _FormulaService;
        private readonly IPersonnelService _PersonnelService;
        private readonly IMapper _mapper;
        public FormulaController(IFormulaService formulaService, IMapper mapper, IPersonnelService personnelService)
        {
            _FormulaService = formulaService;
            _mapper = mapper;
            _PersonnelService = personnelService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateFormula([FromBody] FormulaModel FormulaObj)
        {
            try
            {
                long resultId = 0;
                if (FormulaObj == null)
                {
                    return BadRequest("ابجکت ورودی نال است");
                }
                FormulaDTO newFormula = _mapper.Map<FormulaDTO>(FormulaObj);
                resultId = await _FormulaService.CreateFormulaAsync(newFormula);
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

        [HttpPost("[action]")]
        public async Task<IActionResult> CalculateFormula([FromBody] FormulaCalculationModel formulaModel)
        {
            try
            {
                if(formulaModel == null)
                {
                    return BadRequest("آبجکت ورودی نال است");
                }
                FormulaDTO formula = new FormulaDTO();
                // دریافت لیست فرمول ها از دیتابیس
                List<FormulaDTO> formulaList = await _FormulaService.GetAllFormulasAsync();

                if (formulaList!= null)
                {
                    formula = formulaList.Where( p => p.Id == formulaModel.FormulaId).FirstOrDefault();
                }
                // If formula is found, calculate the result
                List<FieldValueDTO> fieldValues = new List<FieldValueDTO>();
                var personSubmissions = (List<SubmissionDTO>)await _PersonnelService.GetPersonSubmissions(formulaModel.PersonId);

                fieldValues = personSubmissions.Select(p => new FieldValueDTO
                {
                    FieldId = p.Fk_FieldDefinition,
                    Value = p.FieldValue
                }).ToList();
                
                if (formula != null)
                {
                    var result = _FormulaService.CalculateFormulaValue(formula.FormulaText, fieldValues);
                    //return Ok(new { Success = true, Result = result });
                    return Ok(result);
                }

           

                return NotFound();
                //return NotFound(new { Success = false, Message = "Formula not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                //return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }
}
