using static PersonnelManagement.Data.Statics.SD;

namespace PersonnelManagement.API.Models
{
    public class DynamicFieldModel
    {
        public long? Id { get; set; }
        public string? FieldName { get; set; }
        public string? DisplayName { get; set; }
        public FieldType? Type { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
