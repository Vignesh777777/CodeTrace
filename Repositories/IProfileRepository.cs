using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositories
{
    public interface IProfileRepository
    {
        public Task<bool> checkIsProfileComplete(string userEmail);
        public Task<User> getProfile(string? userEmail);
        Task UpdateProfileAsync(string email, User updatedUser);

    }
}
