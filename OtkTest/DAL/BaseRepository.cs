using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace OtkTest.DAL
{
    public abstract class BaseRepository<TKey, TItem> : BaseReadonlyRepository<TKey, TItem>, IRepository<TKey, TItem> where TItem:class where TKey : IComparable
    {        
        protected BaseRepository(DbContext context) : base(context)
        {     
        }

        async Task IRepository<TKey, TItem>.AddAsync(TItem itemToAdd)
        {
            Contract.Requires(itemToAdd != null);
            await DoAddAsync(itemToAdd);
        }

        protected virtual async Task DoAddAsync(TItem itemToAdd)
        {
            await Context.Set<TItem>().AddAsync(itemToAdd);
        }

        async Task IRepository<TKey, TItem>.AddRangeAsync(IEnumerable<TItem> itemsToAdd)
        {
            Contract.Requires(itemsToAdd != null);
            await DoAddRangeAsync(itemsToAdd);
        }

        protected virtual async Task DoAddRangeAsync(IEnumerable<TItem> itemsToAdd)
        {
            await Context.Set<TItem>().AddRangeAsync(itemsToAdd);                        
        }

        async Task IRepository<TKey, TItem>.DeleteAsync(Expression<Func<TItem, bool>> deletePredicate)
        {           
            await DoDeleteAsync(deletePredicate);
        }

        protected virtual async Task DoDeleteAsync(Expression<Func<TItem, bool>> deletePredicate)
        {
            var queryable = Context.Set<TItem>().AsQueryable();
            if (deletePredicate != null)
                queryable = queryable.Where(deletePredicate);

            await queryable.DeleteAsync();
        }

        async Task IRepository<TKey, TItem>.UpdateAsync(Expression<Func<TItem, TItem>> updateFactory, Expression<Func<TItem, bool>> updatePredicate)
        {
            Contract.Requires(updateFactory != null);
            await DoUpdateAsync(updateFactory, updatePredicate);
        }

        protected virtual async Task DoUpdateAsync(Expression<Func<TItem, TItem>> updateFactory, Expression<Func<TItem, bool>> updatePredicate)
        {
            var queryable = Context.Set<TItem>().AsQueryable();
            if (updatePredicate != null)
                queryable = queryable.Where(updatePredicate);

            await queryable.UpdateAsync(updateFactory);
        }
    }
}
