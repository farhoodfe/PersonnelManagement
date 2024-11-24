using PersonnelManagement.MVC.Models.DTOs;

namespace PersonnelManagement.MVC.Models
{
    public class PersonnelViewModel
    {
        public long? PersonId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PersonnelCode { get; set; }
        public List<SubmissionDTO> DynamicFields { get; set; }
    }

}
