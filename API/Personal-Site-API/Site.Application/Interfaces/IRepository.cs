using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> 
    {
        Task<TEntity> GetById(TPrimaryKey id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAll();
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}