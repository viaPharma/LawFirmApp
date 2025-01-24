using Microsoft.EntityFrameworkCore;
using LawFirmApp.Models;

namespace LawFirmApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Role Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
