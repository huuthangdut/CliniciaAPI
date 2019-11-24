using Clinicia.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Clinicia.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        CliniciaDbContext Context { get; }

        ISpecialtyRepository SpecialtyRepository { get; }

        IUserRepository UserRepository { get; }

        IDoctorRepository DoctorRepository { get; }

        IReviewRepository ReviewRepository { get; }

        IFavoriteRepository FavoriteRepository { get; }

        IAppointmentRepository AppointmentRepository { get; }

        IPatientRepository PatientRepository { get; }

        IDeviceRepository DeviceRepository { get; }

        INotificationRepository NotificationRepository { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}