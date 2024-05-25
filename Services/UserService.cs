﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Models;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly TravelBuddyContext _context;

        public UserService(TravelBuddyContext context)
        {
            _context = context;
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
            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = userDto.Password,
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

            if(user == null)
            {
                return null;
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PasswordHash = userDto.Password;

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
    }
}
