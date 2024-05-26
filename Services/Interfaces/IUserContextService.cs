using TravelBuddyAPI.DTOs;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface IUserContextService
    {
        UserResponseDto GetCurrentuser();
    }
}
