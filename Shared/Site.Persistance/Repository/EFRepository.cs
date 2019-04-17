using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TEntity> GetSingleIncluding(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> includes)
        {
            var model = _context.Set<TEntity>().Include(includes).SingleOrDefault(expression);
            return model;
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

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