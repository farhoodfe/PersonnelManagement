using Microsoft.VisualBasic.FileIO;

namespace PersonnelManagement.MVC.Models
{
    public class DynamicFieldDefinition
    {
        public long id { get; set; }
        public string fieldName { get; set; }
        public string displayName { get; set; }
        public int type { get; set; }
        public bool isRequired { get; set; }
        public bool? isDeleted { get; set; }
    }
}
