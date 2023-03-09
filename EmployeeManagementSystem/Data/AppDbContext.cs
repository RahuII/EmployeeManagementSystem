using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employee { get; set; }

    public DbSet<Department> Department { get; set; }
}
