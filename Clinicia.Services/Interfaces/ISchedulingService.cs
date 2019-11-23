using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ISchedulingService
    {
        Task NotifyUpcomingAppointment();
    }
}
