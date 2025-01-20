using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem1.Models
{
    public class Suppliers
    {
        [Key]
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string Address { get; set; }

        public ICollection<Products> Products { get; set; }

        public ICollection<CreditManagement> CreditManagements { get; set; }
    }
}
