using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL.Transaction
{    
    using Models;

    public class TransactionRepository : BaseRepository<long, Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context): base(context)
        { }

        protected override IQueryable<Transaction> DoGetAll()
        {
            return base.DoGetAll()
                .Include(x => x.AccountTypeCommission)
                .Include(x => x.BankCommission)
                .Include(x => x.RecepientAccount)
                .Include(x => x.SenderAccount);
        }
    }
}
