using AutoMapper;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.Models;
using MyEmployees.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        // Get All departments
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            try { 
                var departments = await _departmentRepository.GetAllDepartmentsAsync();
                return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            }
            catch (System.Exception ex)
            {
                // Log the exception (you can use any logging framework)
                System.Console.WriteLine($"An error occurred while retrieving departments: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
            
        }
        // Get department by id
        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            try { 
                var department = await _departmentRepository.GetDepartmentByIdAsync(id);
                return _mapper.Map<DepartmentDto?>(department);
            }
            catch (System.Exception ex)
            {
                // Log the exception (you can use any logging framework)
                System.Console.WriteLine($"An error occurred while retrieving the department with ID {id}: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
        // Add department
        public async Task<DepartmentDto> AddDepartmentAsync(CreateDepartmentDto departmentDto)
        {
            if (departmentDto == null)
                throw new ArgumentNullException(nameof(departmentDto));

            try { 
                var department = _mapper.Map<Department>(departmentDto);
                await _departmentRepository.AddDepartmentAsync(department);
                return _mapper.Map<DepartmentDto>(department);
            }
            catch (System.Exception ex)
            {
                // Log the exception (you can use any logging framework)
                System.Console.WriteLine($"An error occurred while adding a new department: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
        // Update department
        public async Task UpdateDepartmentAsync(int id, CreateDepartmentDto departmentDto)
        {
            if (id == 0)
            {
                throw new System.ArgumentException("Invalid department ID.");
            }
            try
            {
                var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
                if (existingDepartment != null)
                {
                    _mapper.Map(departmentDto, existingDepartment);
                    await _departmentRepository.UpdateDepartmentAsync(existingDepartment);
                }
            }
            catch (System.Exception ex)
            {
                // Log the exception (you can use any logging framework)
                System.Console.WriteLine($"An error occurred while updating the department with ID {id}: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
        // Delete department
        public async Task DeleteDepartmentAsync(int id)
        {
            if (id == 0)
                {
                throw new System.ArgumentException("Invalid department ID.");
            }
            try { 
                await _departmentRepository.DeleteDepartmentAsync(id);
            }
            catch (System.Exception ex)
            {
                // Log the exception (you can use any logging framework)
                System.Console.WriteLine($"An error occurred while deleting the department with ID {id}: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }


        }
    }
}