using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("AccountTypeCommissions")]
    public class AccountTypeCommission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;

        [ForeignKey("SenderAccountType")]
        public int SenderAccountTypeId { get; set; }

        public virtual AccountType SenderAccountType { get; set; }

        [ForeignKey("RecepientAccountType")]
        public int RecepientAccountTypeId { get; set; }

        public virtual AccountType RecepientAccountType { get; set; }

        public float CommissionPercent { get; set; }
       
    }
}
