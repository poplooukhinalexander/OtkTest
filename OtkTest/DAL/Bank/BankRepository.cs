using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace OtkTest.DAL.Bank
{    
    using Models;

    public class BankRepository : BaseReadonlyRepository<int, Bank>, IBankRepository
    {
        public BankRepository(DbContext context) : base(context)
        { }       
    }
}
