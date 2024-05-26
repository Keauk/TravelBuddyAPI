using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripLogService
    {
        Task<TripLog> CreateTripLogAsync(int tripId, TripLogInputDto tripLogInputDto);
        Task<TripLog?> UpdateTripLogAsync(int tripId, TripLogInputDto tripLogInputDto);
        Task<bool> DeleteTripLogAsync(int tripLogId);
        Task<IEnumerable<TripLog>> GetTripLogsForTripAsync(int tripId);
        Task<TripLog?> GetTripLogByIdAsync(int tripId, int tripLogId);
    }
}
