using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Core.Contracts
{
    public interface IRepositoryBase<TEntity>
    {
        /* IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindWithCondition(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveAsyc(); */

        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        // Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveAsync();
    }
}
