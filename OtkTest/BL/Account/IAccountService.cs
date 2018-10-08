using System.Threading.Tasks;

namespace OtkTest.BL.Account
{
    using ViewModels;

    public interface IAccountService
    {
        Task<PagedData<Account>> GetAccountsAsync(int bankId, string accountNumber, int skip, int take);

        Task TransferMoneyAsync(long senderAccountId, long recepeintAccountId, decimal transferAmount);
    }
}
