using Microsoft.EntityFrameworkCore;
using MyEmployees.Api.Models;

namespace MyEmployees.Api.Data
{
    public class MyEmployeesDbContext : DbContext
    {
        public MyEmployeesDbContext(DbContextOptions<MyEmployeesDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);


        }
    }
}