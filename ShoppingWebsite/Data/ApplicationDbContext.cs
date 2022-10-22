using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWebsite.Data;
public class ApplicationDbContext : DbContext
{
    #region DbSet
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    #endregion

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The entity type 'OrderDetail' has multiple properties with the [Key] attribute.
        // Composite primary keys can only be set using 'HasKey' in 'OnModelCreating'.
        modelBuilder.Entity<OrderDetail>()
            .HasKey(orderDetail => new { orderDetail.OrderId, orderDetail.ProductId});

        // create first data for db
        modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Fish", Description = "All about kind of fish" },
                new Category { CategoryId = 2, CategoryName = "Bird", Description = "All about kind of bird" },
                new Category { CategoryId = 3, CategoryName = "Cow", Description = "All about kind of cow" }
            );
        modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, CompanyName = "Cong ty 1", Address = "Quan 1", Phone = "0857817812" },
                new Supplier { SupplierId = 2, CompanyName = "Cong ty 2", Address = "Quan 2", Phone = "0857817812" },
                new Supplier { SupplierId = 3, CompanyName = "Cong ty 3", Address = "Quan 3", Phone = "0857817812" }
            );
    }
}