using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTripsForUser(int userId)
        {
            var trips = await _tripService.GetTripsForUserAsync(userId);

            return Ok(trips);
        }

        // GET: api/trips/5
        [HttpGet("{tripId}")]
        public async Task<ActionResult<Trip>> GetTrip(int tripId)
        {
            var trip = await _tripService.GetTripByIdAsync(tripId);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // GET: api/trips/search?title=someTitle
        [HttpGet("search")]
        public async Task<IActionResult> GetTripsByTitle([FromQuery] TripSearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Trip> trips = await _tripService.SearchTripsByTitleAsync(searchDto.Title);
            if (!trips.Any())
            {
                return NotFound("No trips found matching the given title.");
            }

            return Ok(trips);
        }

        // POST: api/trips
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> PostUser(TripInputDto tripInputDto)
        {
            var createdUser = await _tripService.CreateTripAsync(tripInputDto);

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
        public IActionResult GetCurrentUser()
        {
            if (HttpContext.Items["User"] is not User user)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
