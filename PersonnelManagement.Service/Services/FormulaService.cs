﻿using AutoMapper;
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
        private readonly IFieldDefinitionService _FieldDefinitionService;
        private readonly IMapper _mapper;
        public FormulaService(IRepository<Formula> formula, IMapper mapper, IFieldDefinitionService fieldDefinitionService)
        {
            _mapper = mapper;
            _RFormula = formula;
            _FieldDefinitionService = fieldDefinitionService;   
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

        /// <summary>
        /// متد محسابه فرمول در زمان اجرا
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="fieldValues"></param>
        /// <returns></returns>
        public async Task<string> CalculateFormulaValue(string formula, List<FieldValueDTO> fieldValues)
        {
            try
            {
                // Create a dictionary for field values
                var fieldDict = fieldValues.ToDictionary(fv => fv.FieldId, fv => fv.Value);

                // Replace field placeholders with actual values
                foreach (var field in fieldDict)
                {
                    formula = formula.Replace($"{{Field{field.Key}}}",(field.Value!=null)? field.Value.ToString():field.Value ?? "0");
                    
                }

                // محسابه فرمول بر اساس عملیات ریاضی پایه
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(formula, string.Empty);

                return result.ToString();
            }
            catch (Exception ex)
            {
                // Handle error  
                Console.WriteLine($"Error calculating formula: {ex.Message}");
                return null; // Return null in case of error
            }
        }

        public async Task<List<FormulaDTO>> GetAllFormulasAsync()
        {
            IEnumerable<Formula> fList;
            fList = await _RFormula.GetAllAsync(u => u.IsActive|| u.IsActive== null);

            List<FormulaDTO> formulas= new List<FormulaDTO>();

            foreach (Formula f in fList)
            {
                formulas.Add(_mapper.Map<FormulaDTO>(f));
            }
            _RFormula.SaveAsync();
            return formulas;

        }
    }
}
