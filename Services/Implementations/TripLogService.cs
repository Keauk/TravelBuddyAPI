using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.DTOs;
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

        public async Task<TripLog> CreateTripLogAsync(int tripId, TripLogInputDto tripLogInputDto)
        {
            TripLog tripLog = new()
            {
                TripId  = tripId,
                Location = tripLogInputDto.Location,
                Notes = tripLogInputDto.Notes,
                PhotoPath = tripLogInputDto.PhotoPath,
                Date = tripLogInputDto.Date
            };


            await _context.TripLogs.AddAsync(tripLog);
            await _context.SaveChangesAsync();

            return tripLog;
        }

        public async Task<TripLog?> UpdateTripLogAsync(int tripLogId, TripLogInputDto tripLog)
        {
            TripLog? existingTripLog = await _context.TripLogs.FindAsync(tripLogId);
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

        public async Task<TripLog?> GetTripLogByIdAsync(int tripId, int tripLogId)
        {
            TripLog? existingTripLog = await _context.TripLogs
                .FirstOrDefaultAsync(tl => tl.TripId == tripId && tl.TripLogId == tripLogId);

            if (existingTripLog == null)
            {
                return null;
            }

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
