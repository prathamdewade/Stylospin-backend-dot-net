using Microsoft.Extensions.Logging;
using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly JwtService _jwtService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repo, JwtService jwtService, ILogger<UserService> logger)
        {
            _repo = repo;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<bool> AddUserAsync(TblUser user)
        {
            _logger.LogInformation("Attempting to register user: {Email}", user.UserEmail);

            var existingByEmail = await _repo.GetUserByEmailAsync(user.UserEmail);
            var existingByName = await _repo.GetUserByNameAsync(user.Username);

            if (existingByEmail != null || existingByName != null)
            {
                _logger.LogWarning("Registration failed: user already exists with email or username.");
                return false;
            }

            user.Password = PasswordEncoded.HashPassword(user.Password);
            var result = await _repo.AddUserAsync(user);
            _logger.LogInformation("User registration {Status} for {Email}", result ? "successful" : "failed", user.UserEmail);
            return result;
        }

        public async Task<string> Login(UserLogin dto)
        {
            _logger.LogInformation("Login attempt for: {UserName}", dto.UserName);

            TblUser user = dto.UserName.Contains("@")
                ? await _repo.GetUserByEmailAsync(dto.UserName)
                : await _repo.GetUserByNameAsync(dto.UserName);

            if (user == null)
            {
                _logger.LogWarning("Login failed: User not found - {UserName}", dto.UserName);
                return "User not found";
            }

            bool isValid = PasswordEncoded.VerifyPassword(dto.Password, user.Password);
            if (!isValid)
            {
                _logger.LogWarning("Login failed: Invalid password for {UserName}", dto.UserName);
                return "Invalid password";
            }

            _logger.LogInformation("Login successful for {UserName}", dto.UserName);
            return _jwtService.GenerateToken(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            _logger.LogInformation("Attempting to delete user with ID: {UserId}", userId);

            var user = await _repo.GetUserByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Delete failed: User not found with ID: {UserId}", userId);
                return false;
            }

            var result = await _repo.DeleteUserAsync(user);
            _logger.LogInformation("User deletion {Status} for ID: {UserId}", result ? "successful" : "failed", userId);
            return result;
        }

        public async Task<IEnumerable<TblUser>> GetAllUsersAsync()
        {
            _logger.LogInformation("Fetching all users.");
            var users = await _repo.GetAllUsersAsync();
            _logger.LogInformation("Retrieved {Count} users.", users.Count());
            return users;
        }

        public async Task<bool> UpdateUserAsync(int id, TblUser user)
        {
            _logger.LogInformation("Attempting to update user with ID: {UserId}", id);

            var existingUser = await _repo.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                _logger.LogWarning("Update failed: User not found with ID: {UserId}", id);
                return false;
            }

            user.Id = id;
            var result = await _repo.UpdateUserAsync(user);
            _logger.LogInformation("User update {Status} for ID: {UserId}", result ? "successful" : "failed", id);
            return result;
        }

        public async Task<TblUser> GetUserByIdAsync(int id)
        {
            return await _repo.GetUserByIdAsync(id);
        }

        public async Task<TblUser> GetUserByEmailAsync(string email)
        {
            return await _repo.GetUserByEmailAsync(email);
        }

        public async Task<TblUser> GetUserByNameAsync(string name)
        {
            return await _repo.GetUserByNameAsync(name);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var user = await _repo.GetUserByEmailAsync(email);
            return user != null;
        }
    }
}
