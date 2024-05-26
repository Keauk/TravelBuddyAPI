using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface ITripService
    {
        Task<Trip> CreateTripAsync(TripInputDto trip);
        Task<Trip?> CreateTripForCurrentUserAsync(TripInputDto tripInputDto);
        Task<IEnumerable<Trip>?> GetTripsForUserAsync(int userId);
        Task<Trip?> GetTripByIdAsync(int tripId);
        Task<IEnumerable<Trip>>SearchTripsByTitleAsync(string title);
        Task<Trip?> UpdateTripAsync(int tripId, TripUpdateDto tripUpdateDto);
        Task<bool> DeleteTripAsync(int tripId);
    }
}
