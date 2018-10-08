using System.Data;
using System.Threading.Tasks;

namespace OtkTest.DAL
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        Task<ITransactionScope> BeginTransactionAsync(IsolationLevel isolationLevel);
    }
}
