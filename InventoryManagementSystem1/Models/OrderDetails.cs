using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem1.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Decimal Price { get; set; }

        public Orders Orders {  get; set; } //navigation

        public Products Products { get; set; } //navigation
    }
}
