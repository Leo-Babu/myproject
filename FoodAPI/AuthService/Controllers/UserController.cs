using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthService.Controllers
{
    // [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        public UserController(IUserService userService, IAuthenticationService authService)
        {
            this._userService = userService;
            this._authService = authService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            string token = null;
            try
            {
                token = _authService.Authenticate(user.UserName, user.Password);
                if (token == null)
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return Conflict();
            }
            return Ok(new { token = token });

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Post([FromBody] User value)
        {
            try
            {

                _userService.RegisterUser(value);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
            return StatusCode(StatusCodes.Status200OK, true);
        }

        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            User obj;
            try
            {
                obj = _userService.GetUserByUserName(userName);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(obj);
        }
    }
}
