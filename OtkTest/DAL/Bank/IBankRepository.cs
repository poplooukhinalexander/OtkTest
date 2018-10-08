namespace OtkTest.DAL.Bank
{
    using Models;

    public interface IBankRepository : IReadonlyRepository<int, Bank>
    {
    }
}
