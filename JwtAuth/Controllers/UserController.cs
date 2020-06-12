using JwtAuth.Services;
using JwtAuth.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwtAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userService;

        public UserController(IUserServices userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var user = this.userService.GetUser(HttpContext.DeserializeToken().Email);
            if (user is null)
            {
                return Unauthorized("Token non valido o scaduto");
            }
            return Ok(JsonConvert.SerializeObject(user));
        }
    }
}
