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

            CreateMap<FieldSubmission, SubmissionDTO>();
            CreateMap<SubmissionDTO, FieldSubmission>();

            CreateMap<SubmissionDTO, SubmissionModel>()
                .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.Fk_FieldDefinition ?? 0));
            CreateMap<SubmissionModel, SubmissionDTO>()
                .ForMember(dest => dest.Fk_FieldDefinition, opt => opt.MapFrom(src => (long?)src.FieldId));

            CreateMap<Formula, FormulaDTO>();
            CreateMap<FormulaDTO, Formula>();

            CreateMap<FormulaModel, FormulaDTO>();
            CreateMap<FormulaDTO, FormulaModel>();
        }
    }
}
