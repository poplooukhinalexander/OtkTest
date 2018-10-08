using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace OtkTest.DAL
{
    public interface IRepository<TKey, TItem> : IReadonlyRepository<TKey, TItem> where TItem:class where TKey:IComparable
    {              
        Task AddAsync(TItem itemToAdd);
     
        Task AddRangeAsync(IEnumerable<TItem> itemsToAdd);

        Task UpdateAsync(Expression<Func<TItem, TItem>> updateFactory, Expression<Func<TItem, bool>> updatePredicate = null);

        Task DeleteAsync(Expression<Func<TItem, bool>> deletePredicate = null);        
    }
}
