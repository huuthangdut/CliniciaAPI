using Clinicia.Dtos.Input;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SetLocationAsync(Guid userId, UserLocation location)
        {
            var user = await _unitOfWork.PatientRepository.GetAsync(userId);
            user.Location = new DbLocation
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                FormattedAddress = location.FormattedAddress
            };

            await _unitOfWork.CompleteAsync();
        }
    }
}