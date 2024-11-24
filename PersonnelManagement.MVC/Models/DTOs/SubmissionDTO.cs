namespace PersonnelManagement.MVC.Models.DTOs
{
    public class SubmissionDTO
    {
        public long? FieldId  { get; set; }
        public string FieldName { get; set; } = "";
        public string FieldValue { get; set; } = "";
    }
}
