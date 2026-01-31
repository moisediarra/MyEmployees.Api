using AutoMapper;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.Models;
using MyEmployees.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("An error occurred while retrieving employees.", ex);

            }
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException($"An error occurred while retrieving employee with ID {id}.", ex);
            }
        }

        public async Task AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                await _employeeRepository.AddEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("An error occurred while adding a new employee.", ex);
            }
        }

        public async Task UpdateEmployeeAsync(int id, CreateEmployeeDto UpdateEmployeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(UpdateEmployeeDto);
                await _employeeRepository.UpdateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("An error occurred while updating the employee.", ex);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException($"An error occurred while deleting employee with ID {id}.", ex);
            }
        }
      
    }
}
