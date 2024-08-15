using KuyumcuWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Context;


public class AppDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<ProductImage> productImages { get; set; }
    public DbSet<Address> addresses {get;set;}
    public DbSet<Order> orders {get;set;}
    public DbSet<OrderStatus > orderStatus {get;set;}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Name = "Admin" },
            new Role() { Id = 2, Name = "DefaultUser" }
        );
        modelBuilder.Entity<Role>().ToTable("roles");

        modelBuilder.Entity<Product>()
      .HasMany(p => p.productImages)
      .WithOne(pi => pi.Product)
      .HasForeignKey(pi => pi.ProductId)
      .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<OrderStatus>().HasData(
        new OrderStatus() {
            Id = 1,
            Type = "Bekleyen"
        },
        new OrderStatus(){
            Id = 2,
            Type = "Onaylanan"
        },
        new OrderStatus(){
            Id = 3,
            Type = "Reddedilen"
        }
      );
    }

}