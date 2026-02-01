using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEmployees.Api.Controllers
{
    [Authorize]
    [Route("api/departments")]   // ← good, explicit route
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET all departments
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        // GET single department by id  ← renamed (no Async + meaningful name)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        // POST - create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentDto createDto)
        {
            // Optional: add basic validation if not using FluentValidation
            if (createDto == null) return BadRequest();

            var createdDepartment = await _departmentService.AddDepartmentAsync(createDto);

            // Now points to the correct action name (without Async)
            return CreatedAtAction(
                nameof(GetDepartment),
                new { id = createdDepartment.Id },
                createdDepartment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentDto updateDto)
        {
            var exists = await _departmentService.GetDepartmentByIdAsync(id);
            if (exists == null) return NotFound();

            await _departmentService.UpdateDepartmentAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _departmentService.GetDepartmentByIdAsync(id);
            if (exists == null) return NotFound();

            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}