using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("BankCommissions")]
    public class BankCommission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;

        public DateTime SetupAt { get; set; }

        public float CommissionPercent { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public virtual Bank Bank { get; set; }

        [ForeignKey("BankCommissionType")]
        public int BankCommissionTypeId { get; set; }

        public virtual BankCommissionType BankCommissionType { get; set; }
    }
}
