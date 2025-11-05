using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Models;
using BCrypt.Net;

namespace RTOWebLMS.Services
{
    public class AuthenticationService
    {
        private readonly LmsDbContext _dbContext;

        public AuthenticationService(LmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Authenticate user with email and password
        /// </summary>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return AuthenticationResult.Failed("Email and password are required.");
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return AuthenticationResult.Failed("Invalid email or password.");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                return AuthenticationResult.Failed("Account not configured. Please contact administrator.");
            }

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return AuthenticationResult.Failed("Invalid email or password.");
            }

            // Update last login
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return AuthenticationResult.Success(user);
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        public async Task<AuthenticationResult> RegisterAsync(
            string name,
            string email,
            string password,
            Enums.UserRole role = Enums.UserRole.STUDENT)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(name))
                return AuthenticationResult.Failed("Name is required.");

            if (string.IsNullOrWhiteSpace(email))
                return AuthenticationResult.Failed("Email is required.");

            if (string.IsNullOrWhiteSpace(password))
                return AuthenticationResult.Failed("Password is required.");

            if (password.Length < 6)
                return AuthenticationResult.Failed("Password must be at least 6 characters.");

            // Check if email already exists
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (existingUser != null)
            {
                return AuthenticationResult.Failed("An account with this email already exists.");
            }

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Create new user
            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Role = role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return AuthenticationResult.Success(user);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        public async Task<AuthenticationResult> ChangePasswordAsync(
            string userId,
            string currentPassword,
            string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return AuthenticationResult.Failed("New password is required.");

            if (newPassword.Length < 6)
                return AuthenticationResult.Failed("Password must be at least 6 characters.");

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                return AuthenticationResult.Failed("User not found.");

            if (string.IsNullOrEmpty(user.PasswordHash))
                return AuthenticationResult.Failed("Account not configured.");

            // Verify current password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash);
            if (!isPasswordValid)
                return AuthenticationResult.Failed("Current password is incorrect.");

            // Hash and update new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return AuthenticationResult.Success(user);
        }

        /// <summary>
        /// Reset password for a user (admin function)
        /// </summary>
        public async Task<string> ResetPasswordAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            // Generate temporary password
            string tempPassword = GenerateTemporaryPassword();

            // Hash and update
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return tempPassword;
        }

        /// <summary>
        /// Set password for user (used when creating user via admin panel)
        /// </summary>
        public async Task SetPasswordAsync(string userId, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");

            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters.");

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Generate a random temporary password
        /// </summary>
        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";
            var random = new Random();
            var password = new char[8];

            for (int i = 0; i < password.Length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password);
        }
    }

    /// <summary>
    /// Result of authentication operations
    /// </summary>
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public User? User { get; set; }

        public static AuthenticationResult Success(User user)
        {
            return new AuthenticationResult
            {
                IsSuccess = true,
                User = user
            };
        }

        public static AuthenticationResult Failed(string errorMessage)
        {
            return new AuthenticationResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
