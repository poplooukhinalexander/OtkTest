using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Index("IX_Accounts_Number", 0, IsUnique = true)]
        public string Number { get; set; }

        public decimal Money { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public virtual Bank Bank { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }

        public virtual AccountType AccountType { get; set; }

        [Timestamp]
        public byte[] RowVerion { get; set; }
    }
}
