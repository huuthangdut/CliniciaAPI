using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Implementations
{
    public class CheckingServiceRepository : GenericRepository<DbCheckingService>, ICheckingServiceRepository
    {
        private readonly IMapper _mapper;

        public CheckingServiceRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public DoctorCheckingService[] GetCheckingServices(Guid doctorId)
        {
            return Context.CheckingServices
                .Where(x => x.DoctorId == doctorId)
                .OrderBy(x => x.Name).ThenBy(x => x.Price)
                .ConvertArray(x => _mapper.Map<DoctorCheckingService>(x));
        }
    }
}
