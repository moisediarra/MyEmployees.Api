using Microsoft.EntityFrameworkCore;
using MyEmployees.Api.Data;
using MyEmployees.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyEmployeesDbContext _context;
        private readonly ILogger logger;
        public EmployeeRepository(MyEmployeesDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try 
            {
                return await _context.Employees.Include(e => e.Department).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving all employees.");
                throw;
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving employee with ID {EmployeeId}.", id);
                throw;
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try { 
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                logger.LogInformation("Added new employee with ID {EmployeeId}", employee.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new employee.");
                throw;
            }
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try { 
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                logger.LogInformation("Updated employee with ID {EmployeeId}", employee.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating employee with ID {EmployeeId}.", employee.Id);
                throw;
            }
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            try { 
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Deleted employee with ID {EmployeeId}", id);
                }
                else
                {
                    logger.LogWarning("Attempted to delete non-existent employee with ID {EmployeeId}", id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting employee with ID {EmployeeId}.", id);
                throw;
            }
        }


    }
}
