using Microsoft.EntityFrameworkCore;
using UserManagementRazorViews.Entities;

namespace UserManagementRazorViews.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "VeryBadCompany" },
                new Company { Id = 2, Name = "BadCompany" },
                new Company { Id = 3, Name = "NormCompany" },
                new Company { Id = 4, Name = "GoodCompany" },
                new Company { Id = 5, Name = "VeryGoodCompany" }
            );

            modelBuilder.Entity<UserTitle>().HasKey(ut => new { ut.UserId, ut.TitleId });
            
            modelBuilder.Entity<UserTitle>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UsersTitles)
                .HasForeignKey(ut => ut.UserId);
            
            modelBuilder.Entity<UserTitle>()
                .HasOne(ut => ut.Title)
                .WithMany(u => u.UsersTitles)
                .HasForeignKey(ut => ut.TitleId);
        }
    }
}