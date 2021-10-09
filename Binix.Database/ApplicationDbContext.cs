using Binix.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Binix.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteGroup> RouteGroup { get; set; }
        public DbSet<Cluster> Cluster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("data");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Zone>().HasKey(s => s.Id);
            modelBuilder.Entity<Header>().HasKey(s=>s.Id);
            modelBuilder.Entity<Route>().HasIndex(s=>s.Hostname);
            modelBuilder.Entity<Route>().HasKey(s => s.Id);
            modelBuilder.Entity<RouteGroup>().HasKey(s => s.Id);
            modelBuilder.Entity<Cluster>().HasKey(s => s.Id);
        }
    }
}