using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SurveyWS.Api.Controllers.User.Login
{
    [ApiController]
    [Route("api/user")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseJsonDto>> Index([FromBody] LoginRequestJsonDto data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Email,
                data.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(data);
            }

            return BadRequest("Login incorrecto");
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseJsonDto>> Register(LoginRequestJsonDto data)
        {
            var usuario = new IdentityUser {UserName = data.Email, Email = data.Email};
            var resultado = await _userManager.CreateAsync(usuario, data.Password);

            if (resultado.Succeeded)
            {
                return await BuildToken(data);
            }

            return BadRequest(resultado.Errors);
        }

        private async Task<LoginResponseJsonDto> BuildToken(LoginRequestJsonDto data)
        {
            var claims = new List<Claim>
            {
                new("email", data.Email ?? "")
            };

            var usuario = await _userManager.FindByEmailAsync(data.Email);
            var claimsDb = await _userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new LoginResponseJsonDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiracion
            };
        }
    }
}