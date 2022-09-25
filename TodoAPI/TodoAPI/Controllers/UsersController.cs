using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoAPI.DTOs;
using TodoAPI.Entities;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var user = await _userManager.FindByNameAsync(userCredentials.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, userCredentials.Password))
            {
                var token = GenerateJsonWebToken(user);
                return Ok(token);
            }
            return Unauthorized("Invalid login credentials.");
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserCredentials userLogin)
        {
            var user = new AppUser { UserName = userLogin.UserName };
            var result = await _userManager.CreateAsync(user, userLogin.Password);

            if (result.Succeeded)
            {
                return Ok(GenerateJsonWebToken(user));
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private AuthenticationResponse GenerateJsonWebToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                signingCredentials: credentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = DateTime.Now.AddMinutes(2)
            };
        }
    }
}
