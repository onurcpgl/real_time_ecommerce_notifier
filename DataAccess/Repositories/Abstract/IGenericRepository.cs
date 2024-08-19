using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<T> AddModel(T entity);
        Task<ICollection<T>> AddModels(ICollection<T> entities);
        bool Delete(T entity);
        bool Update(T entity);
        IQueryable<T> GetAll(bool tracking = true);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

        IQueryable<T> GetWhereWithInclude(Expression<Func<T, bool>> method, bool tracking = true, params Expression<Func<T, object>>[] include);

        IQueryable<T> GetAllWithInclude(bool tracking = true, params Expression<Func<T, object>>[] include);
        Task<ICollection<T>> AddRangeAsync(IEnumerable<T> models);
    }
}
