using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;

namespace DataAccess.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RealTimeContext _dbContext;

        public GenericRepository(RealTimeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<T> Table => _dbContext.Set<T>();
        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> Add(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > -1;
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<T> GetWhereWithInclude(Expression<Func<T, bool>> method, bool tracking = true, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = Table;

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            query = query.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public IQueryable<T> GetAllWithInclude(bool tracking = true, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = Table.AsQueryable();
            foreach (var item in include)
            {
                query = query.Include(item);
            }

            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> AddModel(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            _dbContext.SaveChanges();
            return entityEntry.Entity;
        }

        public async Task<ICollection<T>> AddModels(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                EntityEntry<T> entityEntry = Table.Update(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entities;
        }
        public async Task<ICollection<T>> AddRangeAsync(IEnumerable<T> models)
        {
            await Table.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();
            return models.ToList();

        }
    }
}
