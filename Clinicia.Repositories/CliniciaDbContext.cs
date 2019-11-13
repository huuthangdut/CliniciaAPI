using Clinicia.Repositories.Audits;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.Schemas.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Clinicia.Repositories
{
    public class CliniciaDbContext
        : IdentityDbContext<
            DbUser, DbRole, Guid,
            IdentityUserClaim<Guid>, DbUserRole, IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(CliniciaDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
        private readonly IAuditHelper _auditHelper;

        public DbSet<DbAppointment> Appointments { get; set; }

        public DbSet<DbDoctor> Doctors { get; set; }

        public DbSet<DbReview> Reviews { get; set; }

        public DbSet<DbLocation> Locations { get; set; }

        public DbSet<DbNoAttendance> NoAttendances { get; set; }

        public DbSet<DbPatient> Patients { get; set; }

        public DbSet<DbRole> Roles { get; set; }

        public DbSet<DbSpecialty> Specialties { get; set; }

        public DbSet<DbUser> Users { get; set; }

        public DbSet<DbUserRole> UserRoles { get; set; }

        public DbSet<DbWorkingSchedule> WorkingSchedules { get; set; }

        public DbSet<DbFavorite> Favorites { get; set; }

        public DbSet<DbCheckingService> CheckingServices { get; set; }

        public CliniciaDbContext(DbContextOptions options, IAuditHelper auditHelper)
            : base(options)
        {
            _auditHelper = auditHelper;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }

            modelBuilder.Entity<DbUser>(b =>
            {
                b.ToTable("Users");

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<DbRole>(b =>
            {
                b.ToTable("Roles");

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<DbUserRole>(b =>
            {
                b.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("RoleClaims");
            });
        }

        public override int SaveChanges()
        {
            try
            {
                ApplyAuditConcepts();
                var result = base.SaveChanges();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                ApplyAuditConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message, ex);
            }
        }

        protected virtual void ApplyAuditConcepts()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                _auditHelper.ApplyAuditConcepts(entry);
            }
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                /* This condition should normally be defined as below:
                 * !IsSoftDeleteFilterEnabled || !((ISoftDelete) e).IsDeleted
                 * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
                 * So, we made a workaround to make it working. It works same as above.
                 */

                Expression<Func<TEntity, bool>> softDeleteFilter = e => ((ISoftDeleteEntity)e).IsDelete == false;
                expression = softDeleteFilter;
            }

            return expression;
        }
    }
}
