using Microsoft.EntityFrameworkCore;
using late.Data;

namespace late.Data
{
    public class LateContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }


        public LateContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(m =>
            {
                m.ToTable("Post");
                m.Property(p => p.Title).HasMaxLength(256);
                m.Property(p => p.Url).HasMaxLength(256);
            });

            modelBuilder.Entity<Catalog>(m =>
            {
                m.ToTable("Catalog");
                m.Property(c => c.Title).HasMaxLength(64);
                m.Property(c => c.Url).HasMaxLength(256);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<late.Data.Catalog> Catalog { get; set; }
    }
}
