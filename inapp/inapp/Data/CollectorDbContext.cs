using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Data
{
    public class CollectorDbContext : DbContext
    {
        public CollectorDbContext(DbContextOptions<CollectorDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<CollectionItem> CollectionItems { get; set; }
        public DbSet<UserCollection> UserCollection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserCollections)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCollection>()
                .HasMany(uc => uc.CollectionItems)
                .WithOne(ci => ci.UserCollection)
                .HasForeignKey(ci => ci.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
