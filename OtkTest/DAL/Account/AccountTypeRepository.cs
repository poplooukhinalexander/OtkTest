using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL.Account
{
    using Models;

    public class AccountTypeRepository : BaseReadonlyRepository<int, AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(DbContext context) : base(context)
        { }
    }
}
