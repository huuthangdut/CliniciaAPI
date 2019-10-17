using System;
using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Repositories.Implementations;
using Clinicia.Repositories.Interfaces;

namespace Clinicia.Repositories.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ISpecialtyRepository specialtyRepository;

        private readonly IMapper mapper;

        public CliniciaDbContext Context { get; }

        public ISpecialtyRepository SpecialtyRepository => specialtyRepository ?? new SpecialtyRepository(Context, mapper);

        public UnitOfWork(CliniciaDbContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper;
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