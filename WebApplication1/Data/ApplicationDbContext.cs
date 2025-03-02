using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ApplicationUser> Employees { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<PublicHoliday> publicHolidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GeneralSettings>()
                .HasMany(g => g.PublicHolidays)
                .WithOne(p => p.GeneralSettings)
                .HasForeignKey(p => p.GeneralSettingsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
