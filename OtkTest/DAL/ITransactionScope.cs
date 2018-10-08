using System;

namespace OtkTest.DAL
{
    public interface ITransactionScope : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
