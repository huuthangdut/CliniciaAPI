using AutoMapper;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Implementations
{
    public class PatientRepository : GenericRepository<DbPatient>, IPatientRepository
    {
        private readonly IMapper _mapper;

        public PatientRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public void SeedData()
        {
            Context.Specialties.AddRange(
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/heart-rating-128.png",
                    Name = "Dentist"
                },
                new DbSpecialty
                {
                    Image = "https://cdn2.iconfinder.com/data/icons/medical-collection-1/48/chemistry-128.png",
                    Name = "Dentist"
                }
                );

        }
    }
}
