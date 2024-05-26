using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
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
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> PostUser(UserInputDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);

            return Ok(createdUser);
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _userService.LoginAsync(userLoginDto.Username, userLoginDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
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
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("User ID claim is not a valid integer");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
