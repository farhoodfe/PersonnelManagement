﻿using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.Contracts
{
    public interface IFormulaService
    {
        Task<long> CreateFormulaAsync(FormulaDTO formulaDTO);
        Task<List<FormulaDTO>> GetAllFormulasAsync();
        Task<string> CalculateFormulaValue(string formula, List<FieldValueDTO> fieldValues);
    }
}
