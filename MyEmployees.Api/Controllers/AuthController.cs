using Microsoft.AspNetCore.Mvc;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.DTOs.Auth;
using MyEmployees.Api.Services;
using MyEmployees.Api.Services.Auth;

namespace MyEmployees.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _jwtService.GenerateTokenAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Identifiants invalides");
            }
        }
    }
}