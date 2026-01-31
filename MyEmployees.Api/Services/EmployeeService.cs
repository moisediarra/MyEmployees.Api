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

        public async Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null)
                throw new ArgumentNullException(nameof(employeeDto));

            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);

                // Cette ligne ajoute l'employé et fait SaveChanges() → l'ID est généré ici
                await _employeeRepository.AddEmployeeAsync(employee);

                // On retourne le DTO de l'employé créé (avec son nouvel ID)
                return _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                // TODO : utiliser un ILogger en production
                throw new ApplicationException("Une erreur est survenue lors de l'ajout de l'employé.", ex);
            }
        }

        public async Task UpdateEmployeeAsync(int id, CreateEmployeeDto updateDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("L'ID de l'employé est invalide.", nameof(id));
            }

            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            try
            {
                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (existingEmployee == null)
                {
                    throw new KeyNotFoundException($"Employé avec l'ID {id} non trouvé.");
                }

                // Mise à jour des propriétés via AutoMapper
                _mapper.Map(updateDto, existingEmployee);

                // Ou manuellement si tu préfères (plus explicite) :
                // existingEmployee.FirstName = updateDto.FirstName;
                // existingEmployee.LastName  = updateDto.LastName;
                // existingEmployee.Email     = updateDto.Email;
                // existingEmployee.DepartmentId = updateDto.DepartmentId;

                await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
            }
            catch (Exception ex)
            {
                // Log l'erreur ici (ILogger de préférence)
                throw new ApplicationException($"Erreur lors de la mise à jour de l'employé {id}.", ex);
            }
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            if(id == 0)
                {
                throw new ArgumentException("Invalid employee ID.");
            }
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
