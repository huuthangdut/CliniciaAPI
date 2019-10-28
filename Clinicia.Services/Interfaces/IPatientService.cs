using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;

namespace Clinicia.Services.Interfaces
{
    public interface IPatientService
    {
         Task SetLocationAsync(Guid userId, UserLocation location);
    }
}
