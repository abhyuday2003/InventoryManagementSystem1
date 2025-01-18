using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem1.Models
{
    public class Orders
    {
       /* public Orders()
        {
            Status = "Pending";
        } */

        [Key] 
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        public string OrderType { get; set; }

        public Decimal TotalAmount { get; set; }

        [ValidateNever]
        public string Status { get; set; }

        

        [ValidateNever]
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
