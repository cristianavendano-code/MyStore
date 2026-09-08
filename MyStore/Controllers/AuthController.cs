using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Auth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using MyStore.Interfaces;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IClientRepository _clientRepository;

        public AuthController(IConfiguration config, IClientRepository clientRepository)
        {

            _config = config;
            _clientRepository = clientRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var client = _clientRepository.GetClientByEmail(loginDto.Email);

            if (client == null || client.password != loginDto.Password)
            {
                return Unauthorized("Incorrect Credentials");
            }

            var claims = new[]
            {
                //sub : user id
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, client.Email),

                // jti : unique token id
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // firma
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // completed token
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });
        }
    }
}
