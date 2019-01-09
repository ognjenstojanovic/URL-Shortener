using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.EntityFramework
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
        : base(options)
        {

        }

        public UrlShortenerDbContext()
        {
        }

        public virtual DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShortUrl>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.Alias).HasMaxLength(8).IsRequired();

                entity.Property(e => e.OriginalUrl).HasMaxLength(256).IsRequired();
            });
        }
    }
}
