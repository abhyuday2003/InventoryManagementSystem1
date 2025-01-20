using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem1.Models
{
    public class LoginViewModel
    {
        [Key]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
