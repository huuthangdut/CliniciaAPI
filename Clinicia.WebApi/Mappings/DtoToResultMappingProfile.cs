using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.WebApi.Results;
using System;

namespace Clinicia.WebApi.Mappings
{
    public class DtoToResultMappingProfile : Profile
    {
        public DtoToResultMappingProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(x => x.ToString());
            CreateMap<double, double>().ConvertUsing(x => Math.Round(x, 2));
            CreateMap<DateTime, long>().ConvertUsing(x => x.ToSecondsTimestamp());
            CreateMap<TimeSpan, string>().ConvertUsing(x => x.ToString(@"hh\:mm"));

            CreateMap<Specialty, SpecialtyResult>();
            CreateMap<PagedResult<Specialty>, PagedResult<SpecialtyResult>>();

            CreateMap<Doctor, DoctorResult>();
            CreateMap<PagedResult<Doctor>, PagedResult<DoctorResult>>();

            CreateMap<DoctorDetails, DoctorDetailsResult>()
                .IncludeBase<Doctor, DoctorResult>();

            CreateMap<DoctorWorkingTime, DoctorWorkingTimeResult>();
        }
    }
}