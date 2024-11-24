using PersonnelManagement.MVC.Models.DTOs;

namespace PersonnelManagement.MVC.Services.Contracts
{
    public interface IPersonService
    {
        Task<List<PersonnelData>> GetAllPersons();
    }
}
