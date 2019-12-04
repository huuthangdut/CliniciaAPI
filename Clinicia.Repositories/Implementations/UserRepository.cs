using System;
using AutoMapper;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.AspNetCore.Identity;

namespace Clinicia.Repositories.Implementations
{
    public class UserRepository : GenericRepository<DbUser>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            
        }
    }
}
