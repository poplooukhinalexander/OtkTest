using System;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors.TransactionSender
{
    using Models;

    public class PartnerTransactionSender : IBankPostProcessor
    {
        async Task IPostProcessor.ExecuteAsync() => await Task.Run(() => Console.WriteLine("Документ отправлен в банк-партнер."));

        int IPostProcessor.Key { get; } = Bank.Vtb;
    }
}
