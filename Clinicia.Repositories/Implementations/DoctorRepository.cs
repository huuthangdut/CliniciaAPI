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

        public async Task<PagedResult<Doctor>> GetDoctorsAsync(Guid userId, int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions)
        {
            //for (int i = 0; i < 50; i++)
            //{
            //    Context.Doctors.Add(new DbDoctor
            //    {
            //        FirstName = RandomHelper.GetRandomString(5),
            //        LastName = RandomHelper.GetRandomString(5),
            //        UserName = RandomHelper.GetRandomString(10),
            //        Email = RandomHelper.GetRandomString(10) + "@gmail.com",
            //        EmailConfirmed = true,
            //        PhoneNumber = RandomHelper.GetRandomString(10),
            //        PhoneNumberConfirmed = true,
            //        Gender = new Random().Next(1) % 1 == 0,
            //        BirthDate = DateTime.UtcNow,
            //        Price = 250000,
            //        Clinic = RandomHelper.GetRandomString(10),
            //        MedicalSchool = RandomHelper.GetRandomString(20),
            //        Awards = RandomHelper.GetRandomString(20),
            //        YearExperience = RandomHelper.GetRandom(1, 20),
            //        SpecialtyId = RandomHelper.GetRandomOf(new Guid?[] { "27183883-f4c1-486f-9892-a619008ecd69".ParseGuid(), "5ca93936-161e-4f75-9e04-e55c8adba997".ParseGuid(), "8dfb4b83-5401-4eeb-8667-b9b80fc45c27".ParseGuid() }),
            //        IsActive = true,
            //        IsDelete = false
            //    });
            //    Context.SaveChanges();
            //}
            var user = await Context.Patients.Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == userId) ?? throw new EntityNotFoundException();
            
            var query = Context.Doctors
                .IncludeMultiple(x => x.Specialty, x => x.Location, x => x.Reviews)
                .IncludeMultipleIf(
                    filter.AvailableToday.GetValueOrDefault(), 
                    x => x.Appointments, x => x.WorkingSchedules, x => x.NoAttendances)
                .Where(x => x.IsActive)
                .SearchByFields(filter.SearchTerm, x => x.FirstName, x => x.LastName, x => x.Clinic)
                .WhereIf(filter.SpecialtyId.HasValue, x => x.SpecialtyId == filter.SpecialtyId)
                .WhereIf(filter.Gender.HasValue, x => x.Gender == filter.Gender)
                //.WhereIf(filter.PriceFrom.HasValue, x => x.Price >= filter.PriceFrom)
                //.WhereIf(filter.PriceTo.HasValue, x => x.Price <= filter.PriceTo)
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
                    About = x.About,
//                    Price = x.Price,
                    Location = x.Location,
                    Specialty = x.Specialty,
                    Rating = x.Reviews.Any() ? (((decimal?)x.Reviews.Where(r => r.IsActive).Sum(r => r.Rating) / x.Reviews.Count(r => r.IsActive))) : 0,
                    RatingCount = x.Reviews.Count(r => r.IsActive),
                    DistanceFromPatient = LocationHelper.GetDistance(user.Location.Latitude, user.Location.Longitude, x.Location.Latitude, x.Location.Longitude) / 1000
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

        public async Task<DoctorDetails> GetDoctorAsync(Guid userId, Guid doctorId)
        {
            var user = await Context.Patients.Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == userId) ?? throw new EntityNotFoundException();

            var doctor = await Context.Doctors
                .IncludeMultiple(x => x.Specialty, x => x.Reviews, x => x.Location, x => x.Appointments)
                .Where(x => x.Id == doctorId && x.IsActive)
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
                    About = x.About,
//                    Price = x.Price,
                    Location = x.Location,
                    Specialty = x.Specialty,
                    Rating = ((decimal?)x.Reviews.Where(r => r.IsActive).Sum(r => r.Rating) / x.Reviews.Count(r => r.IsActive)) ?? 0,
                    RatingCount = x.Reviews.Count(r => r.IsActive),
                    DistanceFromPatient = LocationHelper.GetDistance(user.Location.Latitude, user.Location.Longitude, x.Location.Latitude, x.Location.Longitude) / 1000,
                    NumberOfPatients = x.Appointments.Where(a => a.IsActive && a.Status == (int)AppointmentStatus.Completed).Select(a => a.PatientId).Count() // Distinct here
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                throw new EntityNotFoundException(typeof(DbDoctor), doctorId);
            }

            return _mapper.Map<DoctorDetails>(doctor);
        }

        public DoctorCheckingService[] GetCheckingServices(Guid id)
        {
            return Context.CheckingServices
                .Where(x => x.IsActive && x.DoctorId == id)
                .ConvertArray(x => _mapper.Map<DoctorCheckingService>(x));
        }

        public async Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date)
        {
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
                        .Where(na => na.IsActive && date.IsBetween(na.FromDate.Date, na.ToDate.Date))
                        .Select(na => TimeRangeUtils.GetTimeRange(na.FromDate, na.ToDate, date))
                        .ToArray(),
                    TimeBusyInDay = x.Appointments
                        .Where(a => a.IsActive && a.Status != (int)AppointmentStatus.Cancelled && a.DoctorId == id && a.AppointmentDate.Date == date.Date)
                        .Select(a => new TimeRange(a.AppointmentDate.TimeOfDay, a.AppointmentDate.AddMinutes(a.TotalMinutes).TimeOfDay))
                        .ToArray()
                })
                .FirstOrDefaultAsync();

            var result = new DoctorWorkingTime
            {
                DoctorId = id,
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
