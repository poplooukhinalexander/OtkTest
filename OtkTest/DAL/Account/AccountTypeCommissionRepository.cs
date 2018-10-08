using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL.Account
{
    using Models;

    public class AccountTypeCommissionRepository : BaseRepository<long, AccountTypeCommission>, IAccountTypeCommissionRepository
    {
        public AccountTypeCommissionRepository(DbContext context) : base(context)
        { }

        async Task<AccountTypeCommission> IAccountTypeCommissionRepository.GetCommissionAsync(int senderAccountType, int recepientAccountType)
        {
            return await DoGetAll()
                .Where(x => x.SenderAccountTypeId == senderAccountType && x.RecepientAccountTypeId == recepientAccountType)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();           
        }
    }
}
