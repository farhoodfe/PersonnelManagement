namespace PersonnelManagement.RazorUI.DTOs
{
    public class DynamicFieldDto
    {
        public long id { get; set; }
        public string fieldName { get; set; }
        public string displayName { get; set; }
        public int type { get; set; }
        public bool isRequired { get; set; }
        public bool? isDeleted { get; set; }
    }
}
