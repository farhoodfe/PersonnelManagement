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
    public class FormulaService : IFormulaService
    {
        private readonly IRepository<Formula> _RFormula;
        private readonly IMapper _mapper;
        public FormulaService(IRepository<Formula> formula, IMapper mapper)
        {
            _mapper = mapper;
            _RFormula = formula;
        }
        public async Task<long> CreateFormulaAsync(FormulaDTO formulaDTO)
        {
            if (formulaDTO == null)
            {
                //ابجکت ورودی نال
                return -1;
            }

            Formula formula = new Formula();
            formula = _mapper.Map<Formula>(formulaDTO);
            await _RFormula.CreateAsync(formula);
            return (formula.Id);
        }
    }
}
