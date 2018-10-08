using System.Threading.Tasks;

namespace OtkTest.BL.Account.PostProcessors
{
    public interface IPostProcessor
    {
        Task ExecuteAsync();
        int Key { get; }
    }
}
