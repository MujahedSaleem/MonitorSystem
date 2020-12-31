using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MonitorSystem.Entity;

namespace MonitorSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>().HasIndex(nameof(Project.ProjectName), nameof(Project.ContractorId)).IsUnique();
            builder.Entity<Movement>().HasOne(a => a.Contractor).WithMany(a => a.Movements).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Movement>().HasOne(a => a.Project).WithMany(a => a.Movements).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Movement>().Property(a => a.Number).UseIdentityColumn();
            builder.Entity<Movement>().Property(a => a.Number).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
