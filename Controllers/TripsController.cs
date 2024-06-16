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
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAllTrips()
        {
            IEnumerable<Trip>? trips = await _tripService.GetAllTrips();

            return Ok(trips);
        }

        // GET: api/trips/5
        [HttpGet("{tripId}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<Trip>> GetTrip(int tripId)
        {
            Trip? trip = await _tripService.GetTripByIdAsync(tripId);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // GET: api/trips/search?title=someTitle
        [HttpGet("search")]
        [Authorize(Policy = "ValidUserPolicy")]
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
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<Trip?>> PostTrip(TripInputDto tripInputDto)
        {
            Trip? createdTrip = await _tripService.CreateTripForCurrentUserAsync(tripInputDto);
            if(createdTrip == null)
            {
                return BadRequest("Could not create the trip.");
            }


            return Ok(createdTrip);
        }

        // PUT: api/trips/5
        [HttpPut("{tripId}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<Trip>> PutTrip(int tripId, TripUpdateDto tripUpdateDto)
        {
            Trip? updatedTrip = await _tripService.UpdateTripAsync(tripId, tripUpdateDto);

            if (updatedTrip == null)
            {
                return NotFound("Trip not found");
            }

            return Ok(updatedTrip);
        }

        // DELETE: api/trips/5
        [HttpDelete("{tripId}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<IActionResult> DeleteUser(int tripId)
        {
            bool result = await _tripService.DeleteTripAsync(tripId);

            if (!result)
            {
                return UnprocessableEntity();
            }

            return Ok();
        }

        // GET: api/trips/me
        [HttpGet("me")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTripsForCurrentUser()
        {
            if (HttpContext.Items["User"] is not UserResponseDto user)
            {
                return NotFound("User not found");
            }

            IEnumerable<Trip>? trips = await _tripService.GetTripsForUserAsync(user.UserId);

            return Ok(trips);
        }
    }
}
