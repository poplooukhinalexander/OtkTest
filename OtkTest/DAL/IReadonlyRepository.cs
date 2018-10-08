using System;
using System.Linq;
using System.Threading.Tasks;

namespace OtkTest.DAL
{
    public interface IReadonlyRepository<TKey, TItem> where TItem : class where TKey : IComparable
    {
        Task<TItem> GetItemAsync(TKey key);

        IQueryable<TItem> GetAll();
    }
}
