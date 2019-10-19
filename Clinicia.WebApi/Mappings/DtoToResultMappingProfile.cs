using AutoMapper;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.WebApi.Results;

namespace Clinicia.WebApi.Mappings
{
    public class DtoToResultMappingProfile : Profile
    {
        public DtoToResultMappingProfile()
        {
            CreateMap<Specialty, SpecialtyResult>();
            CreateMap<PagedResult<Specialty>, PagedResult<SpecialtyResult>>();
        }
    }
}