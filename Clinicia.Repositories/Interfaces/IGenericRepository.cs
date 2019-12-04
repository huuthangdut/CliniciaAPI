using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity Get(Guid id);

        Task<TEntity> GetAsync(Guid id);

        bool Exist(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void AddRange(TEntity[] entities);

        Task AddRangeAsync(TEntity[] entities);

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity AddOrUpdate(TEntity entity);

        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        void Deactive(TEntity entity);

        void Deactive(Guid id);

        void Active(TEntity entity);

        void Active(Guid id);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}