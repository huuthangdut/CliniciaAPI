using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        public CliniciaDbContext Context;

        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public GenericRepository(CliniciaDbContext context)
        {
            Context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Table.AsEnumerable();
        }

        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query.AsEnumerable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate).AsEnumerable();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public TEntity Get(int id)
        {
            var entity = Table.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await Table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Any(predicate);
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Single(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.SingleOrDefault(predicate);
        }
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.SingleAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public void AddRange(TEntity[] entities)
        {
            Table.AddRange(entities);
        }

        public async Task AddRangeAsync(TEntity[] entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public TEntity Add(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(Add(entity));
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            return IsTransient(entity)
                ? Add(entity)
                : Update(entity);
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            return IsTransient(entity)
                ? await AddAsync(entity)
                : await UpdateAsync(entity);
        }

        public void Deactive(TEntity entity)
        {
            if (entity is IActiveableEntity)
            {
                entity.As<IActiveableEntity>().IsActive = false;
            }

            Update(entity);
        }

        public void Deactive(int id)
        {
            var entity = Get(id);

            if (entity != null)
            {
                Deactive(entity);
            }
        }

        public void Active(TEntity entity)
        {
            if (entity is IActiveableEntity)
            {
                entity.As<IActiveableEntity>().IsActive = true;
            }

            Update(entity);
        }

        public void Active(int id)
        {
            var entity = Get(id);

            if (entity != null)
            {
                Active(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public void Delete(int id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = Get(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in Table.Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public int Count()
        {
            return Table.Count();
        }

        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).CountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(int id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<int>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }

        private static bool IsTransient(IEntity entity)
        {
            return entity.Id <= 0;
        }
    }
}