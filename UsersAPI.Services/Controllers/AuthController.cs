using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Authenticate User
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        /// <summary>
        /// Recover User password
        /// </summary>
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }

        /// <summary>
        /// Reset an User password
        /// </summary>
        [HttpPost("reset-password")]
        public IActionResult ResetPassword()
        {
            return Ok();
        }
    }
}
