using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripLogService
    {
        Task<TripLog> CreateTripLogAsync(TripLog tripLog);
        Task<TripLog?> UpdateTripLogAsync(TripLog tripLog);
        Task<bool> DeleteTripLogAsync(int tripLogId);
        Task<IEnumerable<TripLog>> GetTripLogsForTripAsync(int tripId);
    }
}
