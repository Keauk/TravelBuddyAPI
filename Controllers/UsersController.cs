using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/Users/5
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> PostUser(UserInputDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);

            return Ok(createdUser);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserInputDto userDto)
        {
            var user = await _userService.GetRawUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PasswordHash = userDto.Password;

            await _userService.UpdateUserAsync(user);

            return Ok();
        }

        // DELETE: api/Users/5
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
    }
}
