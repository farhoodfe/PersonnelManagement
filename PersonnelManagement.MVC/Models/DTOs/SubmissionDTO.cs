namespace PersonnelManagement.MVC.Models.DTOs
{
    public class SubmissionDTO
    {
        public long? FieldId  { get; set; }
        public string FieldName { get; set; } = "";
        public string? FieldValue { get; set; } = "";
        public int? FieldType { get; set; }
        public DateOnly? DateValue { get; set; }
        public string? InputType
        {
            get => (FieldType!=null)? Enum.GetName(typeof(InputType), FieldType) ?? "text" : null;
        }
    }

    public enum InputType
    {
        text = 0,
        date =2
    }
}
