using Microsoft.EntityFrameworkCore;
using MyEmployees.Api.Data;
using MyEmployees.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyEmployeesDbContext _context;
        private readonly ILogger logger;
        public DepartmentRepository(MyEmployeesDbContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            try
            {
                return await _context.Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving all departments.");
                throw;
            }
        }
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            try
            {
                return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving department with ID {DepartmentId}.", id);
                throw;
            }
        }
        public async Task AddDepartmentAsync(Department department)
        {
            try
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                logger.LogInformation("Added new department with ID {DepartmentId}", department.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new department.");
                throw;
            }
        }
        public async Task UpdateDepartmentAsync(Department department)
        {
            try
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
                logger.LogInformation("Updated department with ID {DepartmentId}", department.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating department with ID {DepartmentId}.", department.Id);
                throw;
            }
        }
        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Deleted department with ID {DepartmentId}", id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting department with ID {DepartmentId}.", id);
                throw;
            }
        }
        
    }
}
