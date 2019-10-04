using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Site.Application.Interfaces;

namespace Site.Persistance.Repository
{
    public class EFRepository<TEntity,TPrimaryKey> : IRepository<TEntity,TPrimaryKey> where TEntity : class
    {
        private readonly SiteDbContext _context;

        public EFRepository(SiteDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetById(TPrimaryKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetSingleIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includes)
            {
                query = query.Include<TEntity, object>(includeProperty);
            }
            
            return await query.SingleOrDefaultAsync(expression);
            
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().Where(expression);
            foreach (Expression<Func<TEntity, object>> includeProperty in includes)
            {
                query = query.Include<TEntity, object>(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetIncludingOfType<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<TEntity>().OfType<T>();
            foreach (Expression<Func<T, object>> includeProperty in includes)
            {
                query = query.Include<T, object>(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetIncluding(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        
        public async Task<IEnumerable<T>> GetIncludingOfType<T>(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<TEntity>().OfType<T>();
            foreach (Expression<Func<T, object>> includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(expression).ToListAsync();
        }

        //public async Task<IEnumerable<TEntity>> GetIncluding(Expression<Func<TEntity, bool>> expression, params string[] includes)
        //{
        //    var query = _context.Set<TEntity>().i(includes);
        //    foreach (string includeProperty in includes)
        //    {

        //        query.Include(includeProperty);
        //    }

        //    return await query.Where(expression).ToListAsync();
        //}

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).ToListAsync();
        }
    }
}