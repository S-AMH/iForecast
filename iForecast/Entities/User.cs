using iForecast.Services.Cypher;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iForecast.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [NotMapped]
        private string _password = null!;
        [Required]
        public string Password { get { return _password; } set { _password = value; } }
        [Required]
        [DisplayName("Joined")]
        public DateTime CreatedDate { get; set; }
        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }
        public void SetPassword(string password)
        {
            _password = _password = HashEngine.HashText(password, Id);
        }
    }
}
