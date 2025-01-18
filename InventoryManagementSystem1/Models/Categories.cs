using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem1.Models
{
    public class Categories
    {
        [Key]

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
