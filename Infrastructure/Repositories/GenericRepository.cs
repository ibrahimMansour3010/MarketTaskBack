using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbSet<T> _table;
        private readonly ApplicationDBConetxt _context;

        public GenericRepository(ApplicationDBConetxt conetxt)
        {
            _context = conetxt;
            _table = _context.Set<T>();
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _table;
            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public async Task<T> Insert(T entity)
        {
            var result = await _context.AddAsync(entity);
            return result.Entity;
        }
        public T Update(T entity)
        {
            return _table.Update(entity).Entity;
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        private IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
        {
            foreach (var include in includeProperties.Split(","))
            {
                query = query.Include(include.Trim());
            }
            return query;
        }
    }
}
