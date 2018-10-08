using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("AccountTypes")]
    public class AccountType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new Collection<Account>();

        [NotMapped]
        public const int IndividualAccountType = 1;

        [NotMapped]
        public const int CompanyAccountType = 2;

        [NotMapped]
        public const int NonResidentAccountType = 3;
    }
}
