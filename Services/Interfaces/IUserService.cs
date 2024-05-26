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
        Task<UserResponseDto?> UpdateUserAsync(int id, UserInputDto userDto);
        Task<bool> DeleteUserAsync(int id);

        Task<string?> LoginAsync(string username, string password);
    }
}
