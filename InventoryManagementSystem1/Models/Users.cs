using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem1.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } //= "Customer";

        public DateTime CreatedAt { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
        public ICollection<Orders> Orders { get; set; }


    }
}
