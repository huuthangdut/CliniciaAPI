using AutoMapper;
using Clinicia.Entities.Register;
using Clinicia.WebApi.Models;

namespace Clinicia.WebApi.Mappings
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<RegisterModel, AccountRegister>();
        }
    }
}