using Clinicia.Entities.Register;
using System.Threading.Tasks;

namespace Clinicia.Abstractions.Services
{
    public interface IRegisterService
    {
        Task RegisterAccountAsync(AccountRegister accountRegister);
    }
}