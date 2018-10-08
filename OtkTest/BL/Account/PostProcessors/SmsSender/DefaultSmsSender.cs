using System;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors.SmsSender
{
    using Models;

    public class DefaultSmsSender : IAccountTypePostProcessor
    {
        async Task IPostProcessor.ExecuteAsync() => await Task.Run(() => Console.WriteLine("СМС отправлена."));

        int IPostProcessor.Key { get; } = AccountType.IndividualAccountType;
    }
}
