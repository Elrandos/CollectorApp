using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Data
{
    public class CollectorDbContext : DbContext
    {
        public CollectorDbContext(DbContextOptions<CollectorDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CollectionItem> CollectionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
