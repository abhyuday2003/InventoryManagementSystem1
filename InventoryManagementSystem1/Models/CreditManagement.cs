using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem1.Models
{
    public class CreditManagement
    {
        [Key]

        public int CreditId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Suppliers")]
        public int SupplierId { get; set; }

        public Decimal OutstandingBalance { get; set; }

        public Decimal CreditLimit { get; set; }

        public DateTime LastUpdated { get; set; }

        public User Users { get; set; } //navigation

        public Suppliers Suppliers { get; set; } //navigation
    }
}
