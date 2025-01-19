using InventoryManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem1.AppData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } 

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Transactions> Transactions { get; set; }

        public DbSet<CreditManagement> CreditManagement { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Admin Credentials
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 3,
                UserName = "admin",
                Password = "Admin@123", // Use a hashed password for production
                Email = "admin@ims.com",
                Role = "Admin",
                CreatedAt = DateTime.Now
            });
        }

    }
}
