using AutoMapper;
using Clinicia.Common;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Projections;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Implementations
{
    public class DoctorRepository : GenericRepository<DbDoctor>, IDoctorRepository
    {
        private readonly IMapper _mapper;

        public DoctorRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<Doctor>> GetDoctorsAsync(int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions)
        {
            var query = Context.Doctors
                .IncludeMultiple(x => x.Specialty, x => x.Location, x => x.Reviews)
                .IncludeMultipleIf(
                    filter.AvailableToday.GetValueOrDefault(), 
                    x => x.Appointments, x => x.WorkingSchedules, x => x.NoAttendances)
                .Where(x => x.IsActive)
                .SearchByFields(filter.SearchTerm, x => x.FirstName, x => x.LastName, x => x.Clinic)
                .WhereIf(filter.Gender.HasValue, x => x.Gender == filter.Gender)
                .WhereIf(filter.PriceFrom.HasValue, x => x.Price >= filter.PriceFrom)
                .WhereIf(filter.PriceTo.HasValue, x => x.Price <= filter.PriceTo)
                .WhereIf(filter.YearExperience.HasValue, GetCompareYearExperiencePredicate(filter.FilterYearExperienceSymbol, filter.YearExperience))
                .Select(x => new DoctorProjection
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    YearExperience = x.YearExperience,
                    Awards = x.Awards,
                    ImageProfile = x.ImageProfile,
                    MedicalSchool = x.MedicalSchool,
                    Clinic = x.Clinic,
                    Price = x.Price,
                    Location = x.Location,
                    Specialty = x.Specialty,
                    Rating = (double?)x.Reviews.Where(r => r.IsActive).Sum(r => r.Rating) / x.Reviews.Count(r => r.IsActive),
                    RatingCount = x.Reviews.Count(r => r.IsActive),
                    DistanceFromPatient = LocationHelper.GetDistance(16, 18, x.Location.Latitude, x.Location.Longitude)
                });

            if(filter.AvailableToday.HasValue)
            {
                // Implement later
            }

            switch (sortOptions?.SortByField)
            {
                case SortDoctorField.Price:
                    query = query.OrderByElseDescending(sortOptions.SortOrder == SortOrder.Asc, x => x.Price);
                    break;
                case SortDoctorField.Distance:
                    query = query.OrderByElseDescending(sortOptions.SortOrder == SortOrder.Asc, x => x.DistanceFromPatient);
                    break;
                case SortDoctorField.StarRating:
                    query = query.OrderByElseDescending(sortOptions.SortOrder == SortOrder.Asc, x => x.Rating);
                    break;
                default:
                    break;
            }

            return await query.GetPagedResultAsync(page, pageSize, x => _mapper.Map<Doctor>(x));
        }

        public async Task<DoctorDetails> GetDoctorAsync(Guid id)
        {
            var doctor = await Context.Doctors
                .IncludeMultiple(x => x.Specialty, x => x.Reviews, x => x.Location, x => x.Appointments)
                .Where(x => x.Id == id && x.IsActive)
                .Select(x => new DoctorDetailsProjection
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    YearExperience = x.YearExperience,
                    Awards = x.Awards,
                    ImageProfile = x.ImageProfile,
                    MedicalSchool = x.MedicalSchool,
                    Clinic = x.Clinic,
                    Price = x.Price,
                    Location = x.Location,
                    Specialty = x.Specialty,
                    Rating = (double?)x.Reviews.Where(r => r.IsActive).Sum(r => r.Rating) / x.Reviews.Count(r => r.IsActive),
                    RatingCount = x.Reviews.Count(r => r.IsActive),
                    DistanceFromPatient = LocationHelper.GetDistance(16, 18, x.Location.Latitude, x.Location.Longitude),
                    NumberOfPatients = x.Appointments.Where(a => a.IsActive && a.Status == (int)AppointmentStatus.Completed).Select(a => a.PatientId).Count() // Distinct here
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                throw new EntityNotFoundException(typeof(DbDoctor), id);
            }

            return _mapper.Map<DoctorDetails>(doctor);
        }

        public async Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date)
        {
            date = date.Date;

            var doctorTime = await Context.Doctors
                .IncludeMultiple(x => x.Appointments, x => x.WorkingSchedules, x => x.NoAttendances)
                .Where(x => x.IsActive && x.Id == id)
                .Select(x => new DoctorTimeProjection
                {
                    WorkingHoursInDay = x.WorkingSchedules
                        .Where(ws => ws.IsActive && date.Date >= ws.FromDate.Date)
                        .Select(ws => ws.Hours.ToWorkingTimes(date.DayOfWeek))
                        .FirstOrDefault(),
                    TimeOffInDay = x.NoAttendances
                        .Where(na => na.IsActive && date.IsBetween(na.FromDate, na.ToDate))
                        .Select(na => TimeRangeUtils.GetTimeRange(na.FromDate, na.ToDate, date))
                        .ToArray(),
                    TimeBusyInDay = x.Appointments
                        .Where(a => a.IsActive && a.Status != (int)AppointmentStatus.Cancelled && a.DoctorId == id && a.DateVisit.Date == date.Date)
                        .Select(a => new TimeRange(a.StartTime, a.EndTime))
                        .ToArray()
                })
                .FirstOrDefaultAsync();

            var result = new DoctorWorkingTime
            {
                DoctorId = id,
                WorkingDate = date,
                WorkingTimes = doctorTime.WorkingHoursInDay
                    .ConvertArray(wh =>
                        TimeRangeUtils.GetTimeFrame(wh, doctorTime.TimeOffInDay, doctorTime.TimeBusyInDay))
                    .ToSingleArray()
            };

            return result;
        }

        private Expression<Func<DbDoctor, bool>> GetCompareYearExperiencePredicate(Symbol? symbol, int? comparedValue)
        {
            if (symbol.HasValue)
            {
                switch (symbol)
                {
                    case Symbol.GreaterThan:
                        return x => x.YearExperience > comparedValue;
                    case Symbol.LessThan:
                        return x => x.YearExperience < comparedValue;
                    case Symbol.Equal:
                    default:
                        return x => x.YearExperience == comparedValue;
                }
            }

            return x => true;
        }
    }
}
