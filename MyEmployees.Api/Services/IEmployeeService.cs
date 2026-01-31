using MyEmployees.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        // ← Changement ici : on retourne maintenant l'employé créé
        Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto);

        Task UpdateEmployeeAsync(int id, CreateEmployeeDto updateEmployeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}