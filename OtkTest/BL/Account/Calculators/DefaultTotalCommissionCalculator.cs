using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.Calculators
{
    using DAL.Account;
    using Models;

    public class DefaultTotalCommissionCalculator : ITotalCommissionCalculator
    {
        private IAccountTypeCommissionRepository AccountTypeCommissionRepository { get; }

        public DefaultTotalCommissionCalculator(IAccountTypeCommissionRepository accountTypeCommissionRepository)
        {
            Contract.Requires(accountTypeCommissionRepository != null);
            AccountTypeCommissionRepository = accountTypeCommissionRepository;
        }

        async Task<decimal> ITotalCommissionCalculator.CalculateCommissionAsync(Account senderAccount, 
            Account recepientAccount, decimal transferAmount)
        {
            return await DoCalculateCommissionAsync(senderAccount, recepientAccount, transferAmount);
        }

        protected virtual async Task<decimal> DoCalculateCommissionAsync(Account senderAccount,
            Account recepientAccount, decimal transferAmount)
        {
            decimal totalCommission = await CalculateAccountTypeCommission(senderAccount.AccountTypeId, recepientAccount.AccountTypeId, transferAmount);
            totalCommission += await CalculateBankCommission(senderAccount.Bank, recepientAccount.Bank, transferAmount);

            return totalCommission;
        }

        protected virtual async Task<decimal> CalculateAccountTypeCommission(int senderAccountType, int recepientAccountType, decimal transferAmount)
        {
            var accountTypeCommission = await AccountTypeCommissionRepository.GetCommissionAsync(senderAccountType, recepientAccountType);
            decimal commission = transferAmount / 100 * (decimal)accountTypeCommission.CommissionPercent;
            return commission;
        }
        protected virtual async Task<decimal> CalculateBankCommission(Bank senderBank, Bank recepientBank, decimal transferAmount)
        {
            return await Task.Run(() => 
            {
                int bankCommissionType = (senderBank.Id == recepientBank.Id) ? BankCommissionType.Internal : BankCommissionType.External;
                var commissionPercent = senderBank.Commissions.Single(x => x.BankCommissionTypeId == bankCommissionType).CommissionPercent;
                var commission = transferAmount / 100 * (decimal)commissionPercent;
                return commission;
            });            
        }
    }
}
