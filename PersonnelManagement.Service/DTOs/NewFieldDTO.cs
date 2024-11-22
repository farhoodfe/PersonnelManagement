using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonnelManagement.Data.Statics.SD;

namespace PersonnelManagement.Service.DTOs
{
    public class NewFieldDTO
    {
        public long Id { get; set; }
        public string? FieldName { get; set; }
        public string? DisplayName { get; set; }
        public FieldType? Type { get; set; }
        public bool? IsRequired { get; set; }

    }
}
