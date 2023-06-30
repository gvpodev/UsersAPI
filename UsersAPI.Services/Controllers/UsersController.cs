using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Create User account
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add()
        {
            return Ok();
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
