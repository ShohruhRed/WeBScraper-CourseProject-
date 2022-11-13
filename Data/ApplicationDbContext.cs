using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeBScraper_CourseProject_.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<News_User>()
                .HasOne(n => n.News)
                .WithMany(nu => nu.News_Users)
                .HasForeignKey(ni => ni.NewsId);

            builder.Entity<News_User>()
                .HasOne(n => n.User)
                .WithMany(nu => nu.News_Users)
                .HasForeignKey(ni => ni.UserId);

            base.OnModelCreating(builder);
        }
        public DbSet<News> NewsDb { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<News_User> News_Users { get; set; }

    }
}