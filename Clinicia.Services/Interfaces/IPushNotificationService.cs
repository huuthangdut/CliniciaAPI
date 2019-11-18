using Clinicia.Dtos.Common;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task Send(string deviceToken, FcmPayloadNotification payload);
    }
}
