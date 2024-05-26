using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TravelBuddyContext _context;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly SymmetricSecurityKey _key;

        public UserService(TravelBuddyContext context, IPasswordHasherService passwordHasher, SymmetricSecurityKey key)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _key = key;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(user => new UserResponseDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedDate = user.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<User?> GetRawUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserInputDto userDto)
        {
            User user = new()
            {
                Username = userDto.Username,
                PasswordHash = _passwordHasher.HashPassword(userDto.Password),
                Email = userDto.Email,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<UserResponseDto?> UpdateUserAsync(int id, UserInputDto userDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PasswordHash = _passwordHasher.HashPassword(userDto.Password);

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
