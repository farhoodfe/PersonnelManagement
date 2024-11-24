using PersonnelManagement.MVC.Models;

namespace PersonnelManagement.MVC.Services.Contracts
{
    public interface IDynamicFieldService
    {
        Task<List<DynamicFieldDefinition>> GetAllFieldsAsync();
        Task<bool> CreateFieldAsync(DynamicFieldDefinition field);
    }
}
