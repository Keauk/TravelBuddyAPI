using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripService
    {
        Task<Trip> CreateTripAsync(Trip trip);
        Task<IEnumerable<Trip>?> GetTripsForUserAsync(int userId);
        Task UpdateTripAsync(Trip trip);
        Task DeleteTripAsync(Trip trip);
    }
}
