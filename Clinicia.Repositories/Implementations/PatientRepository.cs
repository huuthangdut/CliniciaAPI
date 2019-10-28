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
    }
}
