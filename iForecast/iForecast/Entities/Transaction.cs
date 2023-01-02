using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace iForecast.Entities
{
    public class Transaction
    {
        [Key]
        public string Id { get; set; }
        [AllowNull]
        public string? Notes { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string SubAccountId { get; set; } = null!;
        public Transaction()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
