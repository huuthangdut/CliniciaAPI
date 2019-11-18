using AutoMapper;
using Clinicia.Repositories.Implementations;
using Clinicia.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clinicia.Repositories.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        private readonly IDoctorRepository _doctorRepository;

        private readonly IReviewRepository _reviewRepository;

        private readonly IFavoriteRepository _favoriteRepository;

        private readonly IAppointmentRepository _appointmentRepository;

        private readonly IPatientRepository _patientRepository;

        private readonly IDeviceRepository  _deviceRepository;

        private readonly IMapper _mapper;

        public CliniciaDbContext Context { get; }

        public ISpecialtyRepository SpecialtyRepository => _specialtyRepository ?? new SpecialtyRepository(Context, _mapper);

        public IDoctorRepository DoctorRepository => _doctorRepository ?? new DoctorRepository(Context, _mapper);

        public IReviewRepository ReviewRepository => _reviewRepository ?? new ReviewRepository(Context, _mapper);

        public IFavoriteRepository FavoriteRepository => _favoriteRepository ?? new FavoriteRepository(Context, _mapper);

        public IPatientRepository PatientRepository => _patientRepository ?? new PatientRepository(Context, _mapper);

        public IAppointmentRepository AppointmentRepository => _appointmentRepository ?? new AppointmentRepository(Context, _mapper);

        public IDeviceRepository DeviceRepository => _deviceRepository ?? new DeviceRepository(Context);

        public UnitOfWork(CliniciaDbContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}