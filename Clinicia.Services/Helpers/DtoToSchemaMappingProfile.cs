using AutoMapper;
using Clinicia.Dtos.Input;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Services.Helpers
{
    public class DtoToSchemaMappingProfile : Profile
    {
        public DtoToSchemaMappingProfile()
        {
            CreateMap<CreatedAppointment, DbAppointment>();
            CreateMap<CreatedCheckingService, DbCheckingService>();
            CreateMap<UpdatedCheckingService, DbCheckingService>();
        }
    }
}