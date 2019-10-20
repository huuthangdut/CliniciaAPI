﻿using AutoMapper;
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
            CreateMap<DbLocation, Location>();
            CreateMap<DbDoctor, Doctor>();
            CreateMap<DoctorProjection, Doctor>();
            CreateMap<DbSpecialty, Specialty>()
                .ForMember(
                    x => x.NumberOfDoctors,
                    opts => opts.MapFrom(x => x.Doctors.Count));
        }
    }
}