using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Infrastructure;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();        
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly INorthwindContext Context;
        public Repository(INorthwindContext context)
        {
            Context = context;
        }

        public async Task<TEntity> Get(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);            
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        protected IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
    }
}
