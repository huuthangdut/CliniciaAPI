using System;
using System.Threading.Tasks;

namespace Clinicia.Repositories.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public CliniciaDbContext Context { get; }

        public UnitOfWork(CliniciaDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
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