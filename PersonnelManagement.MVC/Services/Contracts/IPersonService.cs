using PersonnelManagement.MVC.Models;

namespace PersonnelManagement.MVC.Services.Contracts
{
    public interface IPersonService
    {
        Task<List<PersonnelViewModel>> GetAllPersons();
    }
}
