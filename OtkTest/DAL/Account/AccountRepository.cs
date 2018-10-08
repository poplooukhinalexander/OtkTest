using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL.Account
{    
    using Models;

    public class AccountRepository : BaseRepository<long, Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        { }

        IQueryable<Account> IAccountRepository.GetAccounts(int bankId, string accountNumber)
        {
            var result = DoGetAll().Where(x => x.BankId == bankId);
            if (string.IsNullOrWhiteSpace(accountNumber))
                return result;

            return result.Where(x => x.Number.StartsWith(accountNumber));
        }

        async Task<Account> IAccountRepository.GetAccountAsync(int accountId)
        {
            return await DoGetAll().SingleOrDefaultAsync(x => x.Id == accountId);
        }


        protected override IQueryable<Account> DoGetAll()
        {
            return base.DoGetAll().Include(x => x.AccountType).Include(x => x.Bank.Commissions);
        }
    }
}
