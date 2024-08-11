using KuyumcuWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Context;


public class AppDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<ProductImage> productImages { get; set; }
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
         .Property(p => p.Id)
         .ValueGeneratedOnAdd(); // Otomatik artırma


        modelBuilder.Entity<Product>()
      .HasMany(p => p.productImages)
      .WithOne(pi => pi.Product)
      .HasForeignKey(pi => pi.ProductId)
      .OnDelete(DeleteBehavior.Cascade);
    }

}