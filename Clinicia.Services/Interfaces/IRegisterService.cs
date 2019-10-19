using System.Threading.Tasks;
using Clinicia.Dtos.Input;

namespace Clinicia.Services.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterAccountAsync(AccountRegister accountRegister);
    }
}