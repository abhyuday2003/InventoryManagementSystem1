using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem1.Models
{
    public class Transactions
    {
        [Key]

        public int TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string TransactionType { get; set; }
    }
}
