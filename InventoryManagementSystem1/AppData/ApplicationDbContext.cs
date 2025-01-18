using InventoryManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem1.AppData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } 

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Transactions> Transactions { get; set; }

        public DbSet<CreditManagement> CreditManagement { get; set; }




    }
}
