using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.DTOs
{
    public class SubmissionDTO
    {
        public long Id { get; set; }
        public string? FieldValue { get; set; }
        public NewFieldDTO FieldDefinition { get; set; }
        
    }
}
