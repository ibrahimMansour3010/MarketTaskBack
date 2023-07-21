using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>>? filter = null,
               Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
               string includeProperties = "");
        Task<T> Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
