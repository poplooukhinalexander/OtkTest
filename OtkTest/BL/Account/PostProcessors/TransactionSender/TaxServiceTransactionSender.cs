using System;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors.TransactionSender
{
    using Models;

    public class TaxServiceTransactionSender : IBankPostProcessor
    {
        async Task IPostProcessor.ExecuteAsync() => await Task.Run(() => Console.WriteLine("Транзакция была отправлена в налоговую."));

        int IPostProcessor.Key { get; } = Bank.Sber;
    }
}
