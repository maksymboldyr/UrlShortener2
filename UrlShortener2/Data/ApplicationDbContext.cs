using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener2.Data.Entities;

namespace UrlShortener2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext(options)
    {
        public DbSet<ShortUrl> ShortenedUrls { get; set; } = null!;
        public DbSet<AboutEntry> AboutEntries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShortUrl>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShortUrl>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

            // Seed roles
            var adminRole = new IdentityRole("ADMIN");
            adminRole.NormalizedName = "ADMIN";

            var userRole = new IdentityRole("USER");
            userRole.NormalizedName = "USER";

            builder.Entity<IdentityRole>().HasData(
                adminRole,
                userRole
            );
        }
    }
}
