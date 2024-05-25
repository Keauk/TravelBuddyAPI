using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripLogService
    {
        Task<TripLog> CreateTripLogAsync(TripLog tripLog);
        Task UpdateTripLogAsync(TripLog tripLog);
        Task DeleteTripLogAsync(TripLog tripLog);
        Task<IEnumerable<TripLog>> GetTripLogsForTripAsync(int tripId);
    }
}
