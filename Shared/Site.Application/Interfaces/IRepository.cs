using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> 
    {
        Task<TEntity> GetById(TPrimaryKey id);
        //Task<TEntity> GetByIdIncluding(TPrimaryKey id, params Expression<Func<TEntity,object>>[] includes);
        Task<TEntity> GetSingleIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity,object>>[] includes);
        Task<IEnumerable<T>> GetIncludingOfType<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<TEntity>> GetIncluding(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<T>> GetIncludingOfType<T>(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes) where T : class;
        //Task<IEnumerable<TEntity>> GetIncluding(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<IEnumerable<TEntity>> GetAll();
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}