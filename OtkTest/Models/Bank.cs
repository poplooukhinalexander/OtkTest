using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LongName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new Collection<Account>();

        public virtual ICollection<BankCommission> Commissions { get; set; } = new Collection<BankCommission>();

        [NotMapped]
        public const int Sber = 1;

        [NotMapped]
        public const int Vtb = 2;

        [NotMapped]
        public const int Alfa = 3;                

        public bool NeedTransactionConfirm { get; set; }
    }
}
