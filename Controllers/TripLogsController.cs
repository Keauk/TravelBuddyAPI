using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/trips/{tripId}/triplogs")]
    public class TripLogsController : ControllerBase
    {
        private readonly ITripLogService _tripLogService;

        public TripLogsController(ITripLogService tripLogService)
        {
            _tripLogService = tripLogService;
        }

        // POST: api/trips/{tripId}/triplogs
        [HttpPost]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<TripLog>> CreateTripLog(int tripId, TripLogInputDto tripLogDto)
        {
            TripLog tripLog = await _tripLogService.CreateTripLogAsync(tripId, tripLogDto);

            return CreatedAtAction(nameof(GetTripLog), new { tripId = tripLog.TripId, tripLogId = tripLog.TripLogId }, tripLog);
        }

        // PUT: api/trips/{tripId}/triplogs/{tripLogId}
        [HttpPut("{tripLogId}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<TripLog>> UpdateTripLog(int tripLogId, TripLogInputDto tripLogDto)
        {
            TripLog? tripLog = await _tripLogService.UpdateTripLogAsync(tripLogId, tripLogDto);
            if (tripLog == null)
            {
                return NotFound("Trip log not found");
            }

            return Ok(tripLog);
        }

        // GET: api/trips/{tripId}/triplogs
        [HttpGet]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<IEnumerable<TripLog?>>> GetTripLogsForTrip(int tripId)
        {
            IEnumerable<TripLog?> tripLogs = await _tripLogService.GetTripLogsForTripAsync(tripId);
            if (tripLogs == null || !tripLogs.Any())
            {
                return NotFound("No trip logs found for this trip");
            }

            return Ok(tripLogs);
        }

        // GET: api/trips/{tripId}/triplogs/{tripLogId}
        [HttpGet("{tripLogId}")]
        [Authorize(Policy = "ValidUserPolicy")]
        public async Task<ActionResult<TripLog?>> GetTripLog(int tripId, int tripLogId)
        {
            var tripLog = await _tripLogService.GetTripLogByIdAsync(tripId, tripLogId);
            if (tripLog == null)
            {
                return NotFound();
            }

            return Ok(tripLog);
        }


    }
}
