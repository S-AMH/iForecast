using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iForecast.Entities
{
    public class Account
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string OwnerId { get; set; } = null!;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }
        public Account()
        {
            this.Id = Guid.NewGuid().ToString();
            Total = 0;
        }
    }
}
