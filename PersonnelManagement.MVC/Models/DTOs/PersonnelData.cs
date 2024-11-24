namespace PersonnelManagement.MVC.Models.DTOs
{
    public class PersonnelData
    {
        public long? id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string personnelCode { get; set; }
        public ICollection<FieldSubmission>? submissions { get; set; }
        //public Dictionary<string, string> DynamicFields { get; set; } // Key: FieldName, Value: FieldValue
    }

}
