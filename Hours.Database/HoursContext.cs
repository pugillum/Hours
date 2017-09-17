using Microsoft.EntityFrameworkCore;
using Hours.Database.Entities;

namespace Hours.Database
{
    public class HoursContext : DbContext
    {
        public DbSet<Task> ProjectTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        

        public HoursContext(DbContextOptions<HoursContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(c => c.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);
        }

        
    }
}
