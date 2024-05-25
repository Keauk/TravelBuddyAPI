using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserDto userDto);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
