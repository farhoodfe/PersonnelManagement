using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.DTOs
{
    public class FormulaDTO
    {
        public long Id { get; set; }
        public string? FormulaName { get; set; }
        public string FormulaText { get; set; } = string.Empty;
    }
}
