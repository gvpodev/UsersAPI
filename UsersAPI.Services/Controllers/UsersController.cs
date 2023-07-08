using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService? _userAppService;

        public UsersController(IUserAppService? userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Create User account
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto), 201)]
        public IActionResult Add([FromBody] UserAddRequestDto request)
        {
            return StatusCode(201, _userAppService?.Add(request));
        }

        /// <summary>
        /// Edit User data
        /// </summary>
        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }


        /// <summary>
        /// Delete User account
        /// </summary>
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

        /// <summary>
        /// Get a single User account
        /// </summary>
        [HttpGet]
        public IActionResult Find()
        {
            return Ok();
        }
    }
}
