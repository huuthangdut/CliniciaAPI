using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Interfaces
{
    public interface IUserService
    {
         Task<JwtTokenResult> SetLocationAsync(Guid userId, UserLocation location);
    }
}
