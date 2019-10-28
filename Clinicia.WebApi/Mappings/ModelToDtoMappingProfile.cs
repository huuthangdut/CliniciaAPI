using System;
using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Input;
using Clinicia.WebApi.Models;

namespace Clinicia.WebApi.Mappings
{
    public class ModelToDtoMappingProfile : Profile
    {
        public ModelToDtoMappingProfile()
        {
            CreateMap<string, Guid?>()
                .ConvertUsing(x => x.ParseNullableGuid());
            CreateMap<string, Guid>()
                .ConvertUsing(x => x.ParseGuid());
            CreateMap<long, DateTime>()
                .ConvertUsing(x => x.FromUnixTimeStamp());

            CreateMap<RegisterModel, AccountRegister>();

            CreateMap<FilterDoctorParams, FilterDoctor>()
                .ForMember(
                    x => x.PriceFrom,
                    opts => opts.MapFrom(x => x.Price.GetPriceRange().PriceFrom)
                    )
                .ForMember(
                    x => x.PriceTo,
                    opts => opts.MapFrom(x => x.Price.GetPriceRange().PriceTo)
                    )
                .ForMember(x => x.YearExperience,
                    opts => opts.MapFrom(x => x.YearExperience.ExtractValue()))
                .ForMember(
                    x => x.FilterYearExperienceSymbol,
                    opts => opts.MapFrom(x => x.YearExperience.GetCompareSymbol()));

            CreateMap<CreatedAppointmentModel, CreatedAppointment>();

            CreateMap<CreatedReviewModel, CreatedReview>();

            CreateMap<UserLocationModel, UserLocation>();
        }
    }
}