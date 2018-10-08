using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OtkTest.BL.Account
{
    using Calculators;
    using DAL;
    using DAL.Account;
    using DAL.Transaction;
    using PostProcessors;
    using ViewModels;

    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; }
        private ITransactionRepository TransactionRepository { get; }       
        private ITotalCommissionCalculator CommissionCalculator { get; }
        private ICommonPostProcessor CommonPostProcessor { get; }
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }

        public AccountService(IAccountRepository accountRepository, 
            IAccountTypeCommissionRepository accountTypeCommissionRepository,
            ITransactionRepository transactionRepository, 
            ITotalCommissionCalculator commissionCalculator,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            Contract.Requires(accountRepository != null);
            Contract.Requires(transactionRepository != null);
            Contract.Requires(accountTypeCommissionRepository != null);
            Contract.Requires(commissionCalculator != null);
            Contract.Requires(unitOfWork != null);
            Contract.Requires(mapper != null);

            AccountRepository = accountRepository;           
            TransactionRepository = transactionRepository;
            CommissionCalculator = commissionCalculator;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        async Task<PagedData<Account>> IAccountService.GetAccountsAsync(int bankId, string accountNumber, int skip, int take)
        {
            using (var transactionScope = await UnitOfWork.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                var accountsQueryable = AccountRepository.GetAccounts(bankId, accountNumber);
                int total = await accountsQueryable.CountAsync();
                var pagedAccounts = await accountsQueryable.Skip(skip).Take(take).ToListAsync();
                var pagedAccountsVm = Mapper.Map<IEnumerable<Account>>(pagedAccounts);

                return new PagedData<Account>(pagedAccountsVm, total);
            }
        }

        async Task IAccountService.TransferMoneyAsync(long senderAccountId, long recepeintAccountId, decimal transferAmount)
        {
            using (var transactionScope = await UnitOfWork.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                var senderAccount = await AccountRepository.GetItemAsync(senderAccountId);
                if (senderAccount == null)
                    throw new System.Exception();

                if (senderAccount.Money < transferAmount)
                    throw new System.Exception();

                var recepientAccount = await AccountRepository.GetItemAsync(recepeintAccountId);
                if (recepientAccount == null)
                    throw new System.Exception();

                var totalCommission = await CommissionCalculator.CalculateCommissionAsync(senderAccount, recepientAccount, transferAmount);
                await AccountRepository.UpdateAsync(x => new Models.Account { Money = senderAccount.Money - transferAmount - totalCommission }, x => x.Id == senderAccountId);
                await AccountRepository.UpdateAsync(x => new Models.Account { Money = recepientAccount.Money + transferAmount }, x => x.Id == recepeintAccountId);

                await CommonPostProcessor.ExecuteAccountTypeProcess(senderAccount.AccountTypeId);
                await CommonPostProcessor.ExecuteBankProcess(senderAccount.BankId);

                await UnitOfWork.SaveChangesAsync();
                transactionScope.Commit();
            }
            
        }
    }
}
