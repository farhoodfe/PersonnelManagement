using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.DTOs
{
    public class PersonInfoDTO
    {
        public long Id { get; set; }
        public string? FName { get; set; } = null;
        public string? LName { get; set; }
        public string? PersonnelCode { get; set; }
    }
}
