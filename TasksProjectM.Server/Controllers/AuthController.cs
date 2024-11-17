using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Services;

namespace TasksProjectM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserForRegistration userForRegistration)
        {
            try
            {
                var token = await _authService.RegisterAsync(userForRegistration);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserForLogin userForLogin)
        {
            try
            {
                var token = await _authService.LoginAsync(userForLogin);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
