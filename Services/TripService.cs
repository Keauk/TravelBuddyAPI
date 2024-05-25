using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Services
{
    public class TripService : ITripService
    {
        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTripAsync(Trip trip)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Trip>> GetTripsForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTripAsync(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
