using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ISmsService
    {
        Task SendAsync(string message, string phoneTo);
    }
}
