using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

public class TripService : ITripService
{
    private readonly TravelBuddyContext _context;
    private readonly IUserContextService _userContext;

    public TripService(TravelBuddyContext context, IUserContextService userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Trip> CreateTripAsync(TripInputDto tripInputDto)
    {
        Trip trip = new()
        {
            Title = tripInputDto.Title,
            Description = tripInputDto.Description,
            StartDate = tripInputDto.StartDate,
            EndDate = tripInputDto.EndDate
        };

        await _context.Trips.AddAsync(trip);
        await _context.SaveChangesAsync();

        return trip;
    }

    public async Task<Trip?> CreateTripForUserAsync(TripInputDto tripInputDto, int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        Trip trip = new()
        {
            UserId = userId,
            Title = tripInputDto.Title,
            Description = tripInputDto.Description,
            StartDate = tripInputDto.StartDate,
            EndDate = tripInputDto.EndDate
        };

        await _context.Trips.AddAsync(trip);
        await _context.SaveChangesAsync();

        return trip;
    }

    public async Task<Trip?> CreateTripForCurrentUserAsync(TripInputDto tripInputDto)
    {
        UserResponseDto? user = _userContext.GetCurrentuser();
        if (user == null)
        {
            return null;
        }

        return await CreateTripForUserAsync(tripInputDto, user.UserId);
    }

    public async Task<IEnumerable<Trip>?> GetTripsForUserAsync(int userId)
    {
        return await _context.Trips.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await _context.Trips
            .Where(t => t.TripId == tripId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Trip>> SearchTripsByTitleAsync(string title)
    {
        return await _context.Trips
            .Where(t => t.Title.Contains(title))
            .ToListAsync();
    }

    public async Task<Trip?> UpdateTripAsync(int tripId, TripUpdateDto tripUpdateDto)
    {
        var existingTrip = await _context.Trips.FindAsync(tripId);
        if (existingTrip == null)
        {
            return null;
        }

        existingTrip.Title = tripUpdateDto.Title;
        existingTrip.Description = tripUpdateDto.Description;
        existingTrip.StartDate = tripUpdateDto.StartDate;
        existingTrip.EndDate = tripUpdateDto.EndDate;

        _context.Entry(existingTrip).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return existingTrip;
    }

    public async Task<bool> DeleteTripAsync(int tripId)
    {
        Trip? existingTrip = await _context.Trips.FindAsync(tripId);
        if (existingTrip == null)
        {
            return false;
        }

        _context.Trips.Remove(existingTrip);

        await _context.SaveChangesAsync();

        return true;
    }

}
