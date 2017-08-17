using System.Collections.Generic;

namespace DataAccess.Models
{
    public class PagedResult<TEntity>
    {
        public int TotalCount { get; }
        public IEnumerable<TEntity> Items { get; }
        public PagedResult(IEnumerable<TEntity> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }                
    }
}
