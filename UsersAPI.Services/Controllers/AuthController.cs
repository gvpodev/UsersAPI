using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService? _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        public IActionResult Login([FromBody] LoginRequestDto request) => StatusCode(200, _authAppService?.Login(request));

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
        [Authorize]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword()
        {
            return Ok();
        }
    }
}
