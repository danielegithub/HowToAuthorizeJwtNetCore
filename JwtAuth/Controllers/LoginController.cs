using System.IdentityModel.Tokens.Jwt;
using JwtAuth.Model;
using JwtAuth.Services;
using JwtAuth.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IUserServices userService;
        public LoginController(IConfiguration config, IUserServices userService)
        {
            this.config = config;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Get([FromQuery]string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Credenziali non inserite");
            }
            if (!this.userService.GetUser(email, password))
            {
                return Unauthorized("Credenziali non valide");
            }

            var token = new JwtSecurityTokenHandler().SetToken(this.config.GetSection("Config")["Key"], new User { Email = email });

            return Ok(new { Token = token });
        }
    }
}