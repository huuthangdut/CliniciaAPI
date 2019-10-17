using AutoMapper;
using Clinicia.Entities.Common;
using Clinicia.Entities.Specialty;
using Clinicia.WebApi.Results;

namespace Clinicia.WebApi.Mappings
{
    public class EntityToResultMappingProfile : Profile
    {
        public EntityToResultMappingProfile()
        {
            CreateMap<Specialty, SpecialtyResult>();
            CreateMap<PagedResult<Specialty>, PagedResult<SpecialtyResult>>();
        }
    }
}