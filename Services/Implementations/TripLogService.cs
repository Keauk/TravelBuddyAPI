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
            await _context.TripLogs.AddAsync(tripLog);
            await _context.SaveChangesAsync();
            return tripLog;
        }

        public async Task<TripLog?> UpdateTripLogAsync(TripLog tripLog)
        {
            var existingTripLog = await _context.TripLogs.FindAsync(tripLog.TripLogId);
            if (existingTripLog == null)
            {
                return null;
            }

            existingTripLog.Location = tripLog.Location;
            existingTripLog.Notes = tripLog.Notes;
            existingTripLog.Date = tripLog.Date;
            existingTripLog.PhotoPath = tripLog.PhotoPath;

            _context.Entry(existingTripLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingTripLog;
        }

        public async Task<bool> DeleteTripLogAsync(int tripLogId)
        {
            var existingTripLog = await _context.TripLogs.FindAsync(tripLogId);
            if (existingTripLog == null)
            {
                return false;
            }

            _context.TripLogs.Remove(existingTripLog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TripLog>> GetTripLogsForTripAsync(int tripId)
        {
            return await _context.TripLogs
                                 .Where(tl => tl.TripId == tripId)
                                 .ToListAsync();
        }
    }
}
