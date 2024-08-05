using KuyumcuWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Context;


class AppDbContext : DbContext
{
    public DbSet<User> users{ get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

}