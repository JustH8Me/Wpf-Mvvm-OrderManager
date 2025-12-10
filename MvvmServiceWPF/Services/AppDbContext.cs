using Microsoft.EntityFrameworkCore;

namespace MvvmServiceWPF.Services;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=orders.db");
    }
}