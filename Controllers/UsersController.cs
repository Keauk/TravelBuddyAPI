using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> PostUser(UserInputDto userDto)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(userDto);
                return Ok(createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _userService.LoginAsync(userLoginDto.Username, userLoginDto.Password);
            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new { Token = token });
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<UserResponseDto>> PutUser(int id, UserInputDto userDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userDto);

            if (updatedUser == null)
            {
                return NotFound("User not found"); 
            }
            
            return Ok(updatedUser);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return UnprocessableEntity();
            }

            return Ok();
        }

        // GET: api/users/me
        [HttpGet("me")]
        [Authorize(Policy = "ValidUserPolicy")]
        public IActionResult GetCurrentUser()
        {
            if (HttpContext.Items["User"] is not UserResponseDto user)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
