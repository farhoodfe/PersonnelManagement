using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelManagement.Data.Entities;
using PersonnelManagement.Service.DTOs;
using PersonnelManagement.API.Models;

namespace PersonnelManagement.Service
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<DynamicFieldDefinition, NewFieldDTO>();
            CreateMap<NewFieldDTO, DynamicFieldDefinition>();

            CreateMap<DynamicFieldModel, NewFieldDTO>();
            CreateMap<NewFieldDTO, DynamicFieldModel>();
        }
    }
}
