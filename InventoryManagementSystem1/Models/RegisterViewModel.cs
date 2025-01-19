using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } = "Customer"; // Default to Customer
    }
}
