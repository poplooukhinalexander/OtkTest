using System;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors.DocumentUploader
{
    using Models;

    public class DocumentUploader : IAccountTypePostProcessor
    {
        async Task IPostProcessor.ExecuteAsync() => await Task.Run(() => Console.WriteLine("Документ был загружен."));

        int IPostProcessor.Key { get; } = AccountType.CompanyAccountType;
    }
}
