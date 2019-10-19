using AutoMapper;
using Clinicia.Dtos.Input;
using Clinicia.WebApi.Models;

namespace Clinicia.WebApi.Mappings
{
    public class ModelToDtoMappingProfile : Profile
    {
        public ModelToDtoMappingProfile()
        {
            CreateMap<RegisterModel, AccountRegister>();
        }
    }
}