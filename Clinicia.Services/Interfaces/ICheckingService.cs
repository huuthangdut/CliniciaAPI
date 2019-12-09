using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ICheckingService
    {
        DoctorCheckingService[] GetCheckingServices(Guid doctorId);

        Task AddCheckingServiceAsync(CreatedCheckingService checkingService);

        Task UpdateCheckingServiceAsync(UpdatedCheckingService checkingService);

        Task DeleteCheckingServiceAsync(Guid id);
    }
}