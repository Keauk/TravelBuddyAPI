using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Services.Implementations
{
    public class TripLogService : ITripLogService
    {
        private readonly TravelBuddyContext _context;

        public TripLogService(TravelBuddyContext context)
        {
            _context = context;
        }

        public async Task<TripLog> CreateTripLogAsync(TripLog tripLog)
        {
            _context.TripLogs.Add(tripLog);
            await _context.SaveChangesAsync();
            return tripLog;
        }

        public async Task UpdateTripLogAsync(TripLog tripLog)
        {
            _context.Entry(tripLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTripLogAsync(TripLog tripLog)
        {
            _context.TripLogs.Remove(tripLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TripLog>> GetTripLogsForTripAsync(int tripId)
        {
            return await _context.TripLogs
                                 .Where(tl => tl.TripId == tripId)
                                 .ToListAsync();
        }
    }
}
