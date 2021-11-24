using BinixPro.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BinixPro.Database;

public class ApplicationDbContext : DbContext
{
#pragma warning disable CS8618
    public DbSet<Route> Routes { get; set; }
    public DbSet<Cluster> Clusters { get; set; }
    public DbSet<Host> Hosts { get; set; }
#pragma warning restore CS8618

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source: binix.db");
    }
}