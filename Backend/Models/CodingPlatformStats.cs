using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CodingPlatformStats
    {
        public int Id { get; set; } // Unique identifier for the stats entry
        [Required]
        public string Platform { get; set; } = string.Empty; // e.g., "LeetCode", "Codeforces", "GitHub"
        [Required]
        public string platformUsername { get; set; } = string.Empty; // Username on the platform (e.g., LeetCode username, GitHub username)
        [Required]
        public string email { get; set; } = string.Empty; // Email of the user to whom the stats belong

        // Foreign Keys
        public int UserId { get; set; }
        public int UserProfileId { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public User? User { get; set; }
        
        [ForeignKey("UserProfileId")]
        public UserProfile? UserProfile { get; set; }

        // Problem counts
        public int EasySolved { get; set; }
        public int MediumSolved { get; set; }
        public int HardSolved { get; set; }
        public int TotalSolved { get; set; }
        public int Score { get; set; }

        // For platforms like Codeforces or CodeChef
        public int TotalSubmissions { get; set; }
        public int TotalContests { get; set; }

        // Rating-based platforms
        public int Rating { get; set; }
        public int MaxRating { get; set; }
        public long Ranking { get; set; }
        public int GlobalRank { get; set; } // e.g., Codeforces rank
        public int CountryRank { get; set; } // e.g., Codeforces country rank
        [Required]
        public string Rank { get; set; } = string.Empty; // e.g., Gfg rank

        // GitHub-specific
        public int PublicRepos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int ContributionsLastYear { get; set; }

        // Timestamp or snapshot date
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
