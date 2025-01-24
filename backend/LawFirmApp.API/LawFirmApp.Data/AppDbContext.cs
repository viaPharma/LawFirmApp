using Microsoft.EntityFrameworkCore;
using LawFirmApp.Models;

namespace LawFirmApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Attorney> Attorneys { get; set; }
    }
}
