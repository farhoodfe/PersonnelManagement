using PersonnelManagement.Data.Repository.Contract;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.DTOs;
using PersonnelManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;

namespace PersonnelManagement.Service.Services
{
    public class FieldDefinitionService : IFieldDefinitionService
    {
        private readonly IRepository<DynamicFieldDefinition> _RFieldDefinition;
        private readonly IMapper _mapper;

        public FieldDefinitionService(IMapper mapper, IRepository<DynamicFieldDefinition> RFieldDefinition)
        {
            _mapper = mapper;
            _RFieldDefinition = RFieldDefinition;
        }
        public async Task<long> CreateFieldAsync(NewFieldDTO newField)
        {
            if (newField == null)
            {
                //ابجکت ورودی نال
                return -1;
            }

            //if (await _RFieldDefinition.GetAsync(u => u.FieldName.ToLower() == newField.FieldName.ToLower()) != null)
            //{
            //    // نام فیلد تکراری است
            //    return -2;
            //}

            DynamicFieldDefinition fd = new DynamicFieldDefinition();
            fd = _mapper.Map<DynamicFieldDefinition>(newField);
            await _RFieldDefinition.CreateAsync(fd);
            return (fd.Id);
        }

        public async Task<ICollection<NewFieldDTO>> GetAllFieldsAsync(int pageSize=0, int pageNumber=1)
        {
            IEnumerable<DynamicFieldDefinition> fieldList;
            fieldList = await _RFieldDefinition.GetAllAsync(u => u.IsDeleted == false || u.IsDeleted== null);

            List<NewFieldDTO> fields = new List<NewFieldDTO>();

            foreach (DynamicFieldDefinition f in fieldList)
            {
                fields.Add( _mapper.Map<NewFieldDTO>(f));
            }

            return fields;


        }

        public async Task<NewFieldDTO> GetFieldById(long id)
        {
            DynamicFieldDefinition field = new DynamicFieldDefinition();
            if (id == 0 )
                return null;
            field = await _RFieldDefinition.FindAsync(id);
            return ( _mapper.Map<NewFieldDTO>(field));
        }


        Task<bool> IFieldDefinitionService.DeleteField(long id)
        {
            throw new NotImplementedException();
        }
    }
}
