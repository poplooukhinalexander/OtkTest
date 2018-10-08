using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("Currencies")]
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }

        [MaxLength(50)]
        public string LongName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new Collection<Account>();
    }
}
