using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Infrastructure.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        Func<TEntity, int> findById { get; set; }

        protected readonly ClashRoyaleContext ClashRoyaleContext;

        public RepositoryBase(ClashRoyaleContext context)
            => ClashRoyaleContext = context;

        public async Task CreateAsync(TEntity entity)
            => await ClashRoyaleContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => ClashRoyaleContext.Set<TEntity>().Remove(entity);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await ClashRoyaleContext.Set<TEntity>()
                                    .ToListAsync();

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
            => ClashRoyaleContext.Set<TEntity>()
                    .IncludeMultiple(includes);

        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var parameterExpression = Expression.Parameter(typeof(TEntity), "object");
            var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, "Id");
            var equalityExpression = Expression.Equal(propertyOrFieldExpression, Expression.Constant(id, typeof(int)));
            var lambdaExpression = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameterExpression);

            var query = ClashRoyaleContext.Set<TEntity>()
                        .IncludeMultiple(includes);

            return await query.SingleOrDefaultAsync(lambdaExpression);

            /*return await ClashRoyaleContext.Set<TEntity>()
                     .FindAsync(id);*/
        }

        public Task SaveAsync()
            => ClashRoyaleContext.SaveChangesAsync();

        public void Update(TEntity entity)
            => ClashRoyaleContext.Entry<TEntity>(entity).State = EntityState.Modified;

        /*public void Create(TEntity entity)
            => ClashRoyaleContext.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity)
            => ClashRoyaleContext.Set<TEntity>().Remove(entity);

        public IQueryable<TEntity> FindAll()
            => ClashRoyaleContext.Set<TEntity>()
                    .AsNoTracking();

        public IQueryable<TEntity> FindWithCondition(Expression<Func<TEntity, bool>> expression)
            => ClashRoyaleContext.Set<TEntity>()
                    .Where(expression)
                    .AsNoTracking();

        public async Task SaveAsyc()
            => await ClashRoyaleContext.SaveChangesAsync();

        public void Update(TEntity entity)
            => ClashRoyaleContext.Set<TEntity>().Update(entity);*/
    }
}
