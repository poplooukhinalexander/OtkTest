using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; }

        [ForeignKey("SenderAccount")]
        public long SenderAccountId { get; set; }

        public virtual Account SenderAccount { get; set; }

        [ForeignKey("RecepientAccount")]
        public long RecepientAccountId { get; set; }

        public Account RecepientAccount { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey("BankCommission")]
        public long BankCommisionId { get; set; }

        public virtual BankCommission BankCommission { get; set; }

        [ForeignKey("AccountTypeCommission")]
        public long AccountTypeCommissionId { get; set; }

        public virtual AccountTypeCommission AccountTypeCommission { get; set; }
    }
}
