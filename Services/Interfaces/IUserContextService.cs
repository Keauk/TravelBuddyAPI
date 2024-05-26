using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface IUserContextService
    {
        User? GetCurrentuser();
    }
}
