using System.Threading.Tasks;

namespace OtkTest.BL.Account.Calculators
{
    using Models;

    public interface ITotalCommissionCalculator
    {
        Task<decimal> CalculateCommissionAsync(Account senderAccount, Account recepientAccount, decimal transferAmount);
    }
}
