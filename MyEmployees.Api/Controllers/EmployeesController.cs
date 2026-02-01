using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Controllers
{
    [Authorize]
    [Route("api/employees/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeService)
        {
            _employeeService = employeService;
        }

        //Get all Employee
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeeAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)          // ← no Async
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }
        //POST
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto createEmployeeDto)
        {
            var employee = await _employeeService.AddEmployeeAsync(createEmployeeDto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }
        // Update
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] CreateEmployeeDto updateDto)
        {
            await _employeeService.UpdateEmployeeAsync(id, updateDto);
            return NoContent();
        }
    }
    }
