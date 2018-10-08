using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OtkTest.Models
{
    public class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Bank> Banks { get; set; }

        public virtual DbSet<AccountType> AccountTypes { get; set; }

        public virtual DbSet<AccountTypeCommission> AccountTypeCommissions { get; set; }

        public virtual DbSet<BankCommission> BankCommissions { get; set; }

        public virtual DbSet<BankCommissionType> BankCommissionTypes { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
                fk.DeleteBehavior = DeleteBehavior.Restrict;         

            base.OnModelCreating(modelBuilder);
        }
    }
}
