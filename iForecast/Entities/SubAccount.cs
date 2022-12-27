using System.ComponentModel.DataAnnotations.Schema;

namespace iForecast.Entities
{
    public class SubAccount
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }
        public string ParentId { get; set; } = null!;
        [NotMapped]
        public Account Parent { get; set; } = null!;
        public SubAccount()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Total = 0;
        }

    }
}