using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Dept_Emp> Dept_Emps { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Dept_Emp>()
                .HasKey(de => new { de.DepartmentId, de.EmployeeId });

            modelBuilder.Entity<Dept_Emp>()
                .HasOne(de => de.Employee)
                .WithMany(e => e.Dept_Emps)
                .HasForeignKey(de => de.EmployeeId);

            modelBuilder.Entity<Dept_Emp>()
                .HasOne(de => de.Department)
                .WithMany(d => d.Dept_Emps)
                .HasForeignKey(de => de.DepartmentId);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }
       
    }
}
