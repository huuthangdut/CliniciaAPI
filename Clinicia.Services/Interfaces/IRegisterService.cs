using Clinicia.Entities.Register;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterAccountAsync(AccountRegister accountRegister);
    }
}