using DepInfoCare.Model;
using Microsoft.EntityFrameworkCore;

namespace DepInfoCare.Data
{
    public class DepInfoCareDbContext : DbContext
    {
        public DepInfoCareDbContext(DbContextOptions<DepInfoCareDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<FacilityModel> Facilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
