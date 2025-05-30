using Backend.Models;

public class User{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string Role { get; set; } = "user"; // Default role is "user"
    public DateTime CreatedAt { get; set; }
    public bool isProfileComplete { get; set; } = false;

    public UserProfile? Profile { get; set; }
    public ICollection<CodingPlatformStats> CodingStats { get; set; } = new List<CodingPlatformStats>();
}