using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors
{
    public interface ICommonPostProcessor
    {
        Task ExecuteAccountTypeProcess(int accountTypeId);

        Task ExecuteBankProcess(int bankId);
    }
}
