using System.Linq;
using System.Threading.Tasks;

namespace OtkTest.DAL.Account
{
    using Models;  

    public interface IAccountRepository : IRepository<long, Account>
    {
        IQueryable<Account> GetAccounts(int bankId, string accountNumber);
        Task<Account> GetAccountAsync(int accountId);
    }
}
