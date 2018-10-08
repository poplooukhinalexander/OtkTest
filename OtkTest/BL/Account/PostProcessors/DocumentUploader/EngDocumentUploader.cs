using System;
using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors.DocumentUploader
{
    using Models;

    public class EngDocumentUploader : IAccountTypePostProcessor
    {
        async Task IPostProcessor.ExecuteAsync() => await Task.Run(() => Console.WriteLine("Документ на английском был загружен."));

        int IPostProcessor.Key { get; } = AccountType.NonResidentAccountType;
    }
}
