using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtkTest.BL.Bank
{  
    using ViewModels;

    public interface IBankService
    {
        Task<IEnumerable<Bank>> GetBanksAsync();
    }
}
