using System.Threading.Tasks;

namespace OtkTest.DAL.Account
{
    using Models;

    public interface IAccountTypeCommissionRepository : IReadonlyRepository<long, AccountTypeCommission>
    {        
        Task<AccountTypeCommission> GetCommissionAsync(int senderAccountType, int recepientAccountType);
    }
}
