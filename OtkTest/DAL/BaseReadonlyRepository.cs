using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL
{
    public abstract class BaseReadonlyRepository<TKey, TItem> : IReadonlyRepository<TKey, TItem> where TItem : class where TKey : IComparable
    {
        protected DbContext Context { get; }

        protected BaseReadonlyRepository(DbContext context)
        {
            Context = context;
        }

        async Task<TItem> IReadonlyRepository<TKey, TItem>.GetItemAsync(TKey key)
        {
            return await DoGetItemAsync(key);
        }

        protected virtual async Task<TItem> DoGetItemAsync(TKey key)
        {
            return await Context.Set<TItem>().FindAsync(key);            
        }
      
        IQueryable<TItem> IReadonlyRepository<TKey, TItem>.GetAll()
        {
            return DoGetAll();
        }

        protected virtual IQueryable<TItem> DoGetAll()
        {
            return Context.Set<TItem>();
        }
    }
}
