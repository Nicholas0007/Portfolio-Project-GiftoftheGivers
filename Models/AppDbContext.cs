using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviationFoundation.Models;


namespace DisasterAlleviationFoundation.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Your existing tables
        public DbSet<DisasterReport> DisasterReports { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
    }
}
