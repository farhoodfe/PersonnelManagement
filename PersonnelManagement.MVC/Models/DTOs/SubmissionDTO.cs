namespace PersonnelManagement.MVC.Models.DTOs
{
    public class SubmissionDTO
    {
        public long? Id  { get; set; }
        public string FieldName { get; set; } = "";
        public string FieldValue { get; set; } = "";
    }
}
