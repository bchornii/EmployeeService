using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Infrastructure;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();        
    }

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly INorthwindContext Context;
        public Repository(INorthwindContext context)
        {
            Context = context;
        }

        public async Task<TEntity> Get(TKey id)
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
