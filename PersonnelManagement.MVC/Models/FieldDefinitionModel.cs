namespace PersonnelManagement.MVC.Models
{
    public class FieldDefinitionModel
    {
        public long? Id { get; set; }
        public string? FieldName { get; set; }
        public string? DisplayName { get; set; }
        public int? Type { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
