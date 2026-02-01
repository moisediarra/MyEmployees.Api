using Microsoft.IdentityModel.Tokens;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.DTOs.Auth;
using MyEmployees.Api.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyEmployees.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> GenerateTokenAsync(LoginDto loginDto)
        {
            // Simulation très basique → À remplacer par vraie vérification en base !
            if (loginDto.Username != "admin" || loginDto.Password != "P@ssw0rd123")
            {
                throw new UnauthorizedAccessException("Identifiants incorrects");
            }

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginDto.Username),
                new Claim(ClaimTypes.Role, "Admin"),           // ou "User", etc.
                new Claim(JwtRegisteredClaimNames.Sub, loginDto.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiresInMinutes = int.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "30");

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo
            };
        }
    }
}