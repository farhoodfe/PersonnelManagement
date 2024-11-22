using PersonnelManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Service.Contracts
{
    public interface IFieldDefinitionService
    {
        int CreateField(NewFieldDTO field);
        int UpdateField(long id, NewFieldDTO field);
        bool DeleteField(long id);
        ICollection<NewFieldDTO> GetAllFieldsAsync();
    }
}
