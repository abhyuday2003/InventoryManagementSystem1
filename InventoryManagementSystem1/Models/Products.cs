using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        [ForeignKey("Suppliers")]
        public int SupplierId { get; set; }

        public Decimal UnitPrice { get; set; }

        public int QuantityStock { get; set; }

        public int ReorderLevel { get; set; }
        public Categories Categories { get; set; }

        public Suppliers Suppliers { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
