using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;

namespace TravelBuddyAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserInputDto userDto);
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<User?> GetRawUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
