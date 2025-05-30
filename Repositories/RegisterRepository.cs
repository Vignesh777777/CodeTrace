using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Repositories;
public class RegisterRepository : IRegisterRepository
{
    private readonly ApplicationDbContext _context;
    public RegisterRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<User> CreateUser(User user)
    {
        // Create a new UserProfile and associate it with the user
        user.Profile = new UserProfile
        {
            // Initialize with default values or empty strings
            name = "",
            graduationYear = 0,
            department = "",
            phoneNumber = "",
            section = "",
            rollNumber = "",
            skills = new List<string>(),
            interests = new List<string>(),
            description = "",
            // Other profile properties can be initialized here as needed
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User> GetUser(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<bool> SetUserRole(string email, string role)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return false;
            
        user.Role = role;
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }
}