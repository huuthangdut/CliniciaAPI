using System.Threading.Tasks;
using Clinicia.Dtos.Input;

namespace Clinicia.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<string> RegisterAccountAsync(AccountRegister accountRegister);

        Task<string> RegisterDoctorAsync(AccountDoctorRegister accountDoctorRegister);
    }
}