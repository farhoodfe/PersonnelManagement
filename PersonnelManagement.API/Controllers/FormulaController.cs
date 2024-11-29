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
    public class FormulaController : ControllerBase
    {
        private readonly IFormulaService _FormulaService;
        private readonly IMapper _mapper;
        public FormulaController(IFormulaService formulaService, IMapper mapper)
        {
            _FormulaService = formulaService;
            _mapper = mapper;
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

    }
}
