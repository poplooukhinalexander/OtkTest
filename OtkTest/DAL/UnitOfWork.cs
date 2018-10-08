using System;
using System.Data;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace OtkTest.DAL
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbContext Context { get; }      

        public UnitOfWork(DbContext context)
        {
            Context = context;          
        }

        async Task<ITransactionScope> IUnitOfWork.BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            return await TransactionScope.CreateTransactionScopeAsync(Context, isolationLevel);
        }

        async Task<int> IUnitOfWork.SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();                      
        }

        private class TransactionScope : ITransactionScope
        {
            private IDbContextTransaction DbTransaction { get; }

            private bool disposed;

            private TransactionScope(IDbContextTransaction dbTransaction)
            {
                DbTransaction = dbTransaction;
            }    

            public static async Task<ITransactionScope> CreateTransactionScopeAsync(DbContext context, IsolationLevel isolationLevel)
            {
                var dbTransaction = await context.Database.BeginTransactionAsync(isolationLevel);
                var transactionScope = new TransactionScope(dbTransaction);
                return transactionScope; 
            }

            void ITransactionScope.Commit()
            {
                DbTransaction.Commit();
            }

            void ITransactionScope.Rollback()
            {
                DbTransaction.Rollback();
            }

            void Dispose(bool disposing)
            {
                if (disposed || !disposing)
                    return;

                if (DbTransaction != null)                                   
                    DbTransaction.Dispose();
                
                disposed = true;
            }

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
