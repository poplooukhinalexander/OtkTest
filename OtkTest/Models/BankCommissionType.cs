using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtkTest.Models
{
    [Table("BankCommissionTypes")]
    public class BankCommissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [NotMapped]
        public const int Internal = 1;

        [NotMapped]
        public const int External = 2;
    }
}
