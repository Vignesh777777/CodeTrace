using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> checkIsProfileComplete(string userEmail)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user?.Profile == null)
                return false;

            // Check if all required fields are filled
            return !string.IsNullOrEmpty(user.Profile.name) &&
                   user.Profile.graduationYear > 0 &&
                   !string.IsNullOrEmpty(user.Profile.department) &&
                   !string.IsNullOrEmpty(user.Profile.phoneNumber) &&
                   !string.IsNullOrEmpty(user.Profile.section) &&
                   !string.IsNullOrEmpty(user.Profile.rollNumber) &&
                   !string.IsNullOrEmpty(user.Profile.description) &&
                   user.Profile.skills.Any();
        }

        public async Task<User?> getProfile(string? userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                return null;

            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == userEmail);
            return user;
        }

        public async Task UpdateProfileAsync(string email, User updatedUser)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return;

            if (user.Profile == null)
            {
                user.Profile = new Models.UserProfile();
            }

            // Update profile properties
            if (updatedUser.Profile != null)
            {
                user.Profile.name = updatedUser.Profile.name;
                user.Profile.graduationYear = updatedUser.Profile.graduationYear;
                user.Profile.department = updatedUser.Profile.department;
                user.Profile.phoneNumber = updatedUser.Profile.phoneNumber;
                user.Profile.section = updatedUser.Profile.section;
                user.Profile.rollNumber = updatedUser.Profile.rollNumber;
                user.Profile.description = updatedUser.Profile.description;
                user.Profile.skills = updatedUser.Profile.skills;
                user.Profile.interests = updatedUser.Profile.interests;

                // Update coding platform usernames if provided
                if (!string.IsNullOrEmpty(updatedUser.Profile.leetcodeUsername))
                    user.Profile.leetcodeUsername = updatedUser.Profile.leetcodeUsername;
                if (!string.IsNullOrEmpty(updatedUser.Profile.githubUsername))
                    user.Profile.githubUsername = updatedUser.Profile.githubUsername;
                if (!string.IsNullOrEmpty(updatedUser.Profile.codechefUsername))
                    user.Profile.codechefUsername = updatedUser.Profile.codechefUsername;
                if (!string.IsNullOrEmpty(updatedUser.Profile.hackerrankUsername))
                    user.Profile.hackerrankUsername = updatedUser.Profile.hackerrankUsername;
                if (!string.IsNullOrEmpty(updatedUser.Profile.codeforcesUsername))
                    user.Profile.codeforcesUsername = updatedUser.Profile.codeforcesUsername;
                if (!string.IsNullOrEmpty(updatedUser.Profile.gfgUsername))
                    user.Profile.gfgUsername = updatedUser.Profile.gfgUsername;
            }

            // Update profile completion status
            user.isProfileComplete = await checkIsProfileComplete(email);

            await _context.SaveChangesAsync();
        }
    }
}

