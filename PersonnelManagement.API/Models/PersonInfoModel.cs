using PersonnelManagement.Service.DTOs;

namespace PersonnelManagement.API.Models
{
    public class PersonInfoModel
    {
        public long? Id { get; set; }
        public string? FName { get; set; } = null;
        public string? LName { get; set; }
        public string? PersonnelCode { get; set; }
        public IEnumerable<SubmissionModel> Submissions { get; set; }
    }
}
