using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors
{
    public class CommonPostProcessor : ICommonPostProcessor
    {
        private IEnumerable<IAccountTypePostProcessor> AccountTypePostProcessors { get; }

        private IEnumerable<IBankPostProcessor> BankPostProcessors { get; }

        public CommonPostProcessor(IEnumerable<IAccountTypePostProcessor> accountTypePostProcessors,
            IEnumerable<IBankPostProcessor> bankPostProcessors)
        {
            Contract.Requires(accountTypePostProcessors != null);
            Contract.Requires(BankPostProcessors != null);

            AccountTypePostProcessors = accountTypePostProcessors;
            BankPostProcessors = bankPostProcessors;
        }

        async Task ICommonPostProcessor.ExecuteAccountTypeProcess(int accountTypeId)
        {
            await DoExecutePostProcess(AccountTypePostProcessors, accountTypeId);
        }

        async Task ICommonPostProcessor.ExecuteBankProcess(int bankId)
        {
            await DoExecutePostProcess(BankPostProcessors, bankId);
        }

        protected virtual async Task DoExecutePostProcess(IEnumerable<IPostProcessor> postProcessors, int id)
        {
            foreach (var postProcessor in postProcessors.Where(x => x.Key == id))
            {
                await postProcessor.ExecuteAsync();
            }
        }        
    }
}
