using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using platformservice.viewmodels;

namespace platformservice.controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody]AuthRequest request)
        {
            if (request.UserName != "test" || request.Password != "test")
            {
                return Unauthorized();
            }
            var token =GeneratejwtToken(request.UserName);
            return Ok(token);

            
        }
        public AuthResponse GeneratejwtToken(string userName)
        {
            var claims= new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key= _configuration["jwt:key"];
            var issuer= _configuration["jwt:issuer"];   
            var audience= _configuration["jwt:audience"];
            var securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials= new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var token= new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            var tokenString= new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthResponse
            {
                Token = tokenString,
                Expiration = token.ValidTo,
                Type = "Bearer"
            };
        }
    }
}