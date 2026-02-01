using MyEmployees.Api.DTOs.Auth;

namespace MyEmployees.Api.Services.Auth
{
    public interface IJwtService
    {
        Task<AuthResponseDto> GenerateTokenAsync(LoginDto loginDto);
    }
}
