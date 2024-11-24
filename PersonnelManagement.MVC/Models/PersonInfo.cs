namespace PersonnelManagement.MVC.Models
{
    public class PersonInfo
    {
        public long? Id { get; set; }
        public string? FName { get; set; } = null;
        public string? LName { get; set; }
        public string? PersonnelCode { get; set; }
        public IEnumerable<FieldSubmission> Submissions { get; set; }
    }
}
