using AutoMapper;
using Clinicia.Common.Extensions;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Projections;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Services.Helpers
{
    public class SchemaToDtoMappingProfile : Profile
    {
        public SchemaToDtoMappingProfile()
        {
            CreateMap<DbSpecialty, DictionaryItem>();
            CreateMap<DbLocation, Location>()
                .ForMember(
                    x => x.Address,
                    opts => opts.MapFrom(x => x.FormattedAddress)
                    );
            CreateMap<DbDoctor, Doctor>();
            CreateMap<DoctorProjection, Doctor>();
            CreateMap<DbSpecialty, Specialty>()
                .ForMember(
                    x => x.NumberOfDoctors,
                    opts => opts.MapFrom(x => x.Doctors.Count));
            CreateMap<DoctorDetailsProjection, DoctorDetails>()
                .IncludeBase<DoctorProjection, Doctor>();

            CreateMap<DbPatient, DictionaryItem>()
                .ForMember(
                    x => x.Name,
                    opts => opts.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<DbReview, DoctorReview>();

            CreateMap<UserFavoriteProjection, UserFavorite>();
            CreateMap<FavoriteDoctorProjection, FavoriteDoctor>();

            CreateMap<DbAppointment, Appointment>();
            CreateMap<DbDoctor, AppointmentDoctor>()
                .ForMember(x => x.Name, opts => opts.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                .ForMember(x => x.Address,
                    opts => opts.MapFrom(x => x.Location.FormattedAddress))
                .ForMember(x => x.Longitude, opts => opts.MapFrom(x => x.Location.Longitude))
                .ForMember(x => x.Latitude, opts => opts.MapFrom(x => x.Location.Latitude))
                .ForMember(x => x.Specialty, opts => opts.MapFrom(x => x.Specialty.Name));

            CreateMap<DbCheckingService, DoctorCheckingService>()
                .ForMember(x => x.Price, opts => opts.MapFrom(x => x.Price.RoundTo(2)));

            CreateMap<DbNotification, Notification>();

            CreateMap<DbUser, UserLoginInfo>();

            CreateMap<DbAppointment, DoctorAppointment>();
            CreateMap<DbPatient, AppointmentPatient>()
                .ForMember(x => x.Name, opts => opts.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                .ForMember(x => x.Address,
                    opts => opts.MapFrom(x => x.Location.FormattedAddress))
                .ForMember(x => x.Longitude, opts => opts.MapFrom(x => x.Location.Longitude))
                .ForMember(x => x.Latitude, opts => opts.MapFrom(x => x.Location.Latitude));
        }
    }
}