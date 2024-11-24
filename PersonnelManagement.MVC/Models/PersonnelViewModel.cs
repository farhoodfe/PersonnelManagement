namespace PersonnelManagement.MVC.Models
{
    public class PersonnelViewModel
    {
        public long? PersonId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PersonnelCode { get; set; }
        public Dictionary<string, string> DynamicFields { get; set; } // Key: FieldName, Value: FieldValue
    }

}
