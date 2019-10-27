using AutoMapper;
using Clinicia.Common.Extensions;
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

            CreateMap<DoctorReview, DoctorReviewResult>();
            CreateMap<PagedResult<DoctorReview>, PagedResult<DoctorReviewResult>>();

            CreateMap<FavoriteDoctor, FavoriteDoctorResult>();
            CreateMap<UserFavorite, UserFavoriteResult>();
            CreateMap<PagedResult<UserFavorite>, PagedResult<UserFavoriteResult>>();

            CreateMap<AppointmentDoctor, AppointmentDoctorResult>();
            CreateMap<Appointment, AppointmentResult>()
                .ForMember(
                    x => x.Price,
                    opts => opts.MapFrom(x => x.Price.RoundTo(2))
                    );
            CreateMap<PagedResult<Appointment>, PagedResult<AppointmentResult>>();
        }
    }
}