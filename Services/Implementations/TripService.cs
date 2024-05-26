using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

public class TripService : ITripService
{
    private readonly TravelBuddyContext _context;
    private readonly IUserService _userService;

    public TripService(TravelBuddyContext context, IUserService userContextService)
    {
        _context = context;
        _userService = userContextService;
    }

    public async Task<Trip> CreateTripAsync(Trip trip)
    {
        await _context.Trips.AddAsync(trip);
        await _context.SaveChangesAsync();

        return trip;
    }

    public async Task<Trip?> CreateTripForUserAsync(Trip trip, int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        trip.UserId = userId;

        return await CreateTripAsync(trip);
    }

    public async Task<Trip?> CreateTripForCurrentUserAsync(Trip trip)
    {
        User? user = await _userService.GetCurrentUser();
        if (user == null)
        {
            return null;
        }

        int currentUserId = user.UserId;

        return await CreateTripForUserAsync(trip, currentUserId);
    }

    public async Task<IEnumerable<Trip>?> GetTripsForUserAsync(int userId)
    {
        return await _context.Trips.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Trip?> UpdateTripAsync(Trip trip)
    {
        var existingTrip = await _context.Trips.FindAsync(trip.TripId);
        if (existingTrip == null)
        {
            return null;
        }

        existingTrip.Title = trip.Title;
        existingTrip.Description = trip.Description;
        existingTrip.StartDate = trip.StartDate;
        existingTrip.EndDate = trip.EndDate;

        await _context.SaveChangesAsync();

        return existingTrip;
    }


    public async Task<bool> DeleteTripAsync(Trip trip)
    {
        var existingTrip = await _context.Trips.FindAsync(trip.TripId);
        if (existingTrip == null)
        {
            return false;
        }

        _context.Trips.Remove(existingTrip);
        await _context.SaveChangesAsync();

        return true;
    }
}
