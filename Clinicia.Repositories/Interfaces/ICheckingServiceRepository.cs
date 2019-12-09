using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;

namespace Clinicia.Repositories.Interfaces
{
    public interface ICheckingServiceRepository : IGenericRepository<DbCheckingService>
    {
        DoctorCheckingService[] GetCheckingServices(Guid doctorId);
    }
}