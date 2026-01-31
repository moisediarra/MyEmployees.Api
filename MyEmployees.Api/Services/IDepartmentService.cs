using MyEmployees.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> AddDepartmentAsync(CreateDepartmentDto departmentDto);
        Task UpdateDepartmentAsync(int id, CreateDepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);
    }
}
