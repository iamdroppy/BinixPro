using BinixPro.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BinixPro.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Zone> Zones { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Incoming> Incomings { get; set; }
    public DbSet<Outgoing> Outgoings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source: binix.db");
    }
}