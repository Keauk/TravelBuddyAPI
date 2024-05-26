using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripService
    {
        Task<Trip> CreateTripAsync(Trip trip);
        Task<IEnumerable<Trip>?> GetTripsForUserAsync(int userId);
        Task<Trip?> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(Trip trip);
    }
}
