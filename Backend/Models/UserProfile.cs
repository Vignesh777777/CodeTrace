using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string name { get; set; } = string.Empty;
        [Range(2000, 2100, ErrorMessage = "Graduation year must be between 2000 and 2100.")]
        public int graduationYear { get; set; }
        [Required]
        [StringLength(50)]
        public string department { get; set; } = string.Empty;
        [Required]
        [Phone]
        [StringLength(15)]
        public string phoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        public string section { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string rollNumber { get; set; } = string.Empty;
        [StringLength(50)]
        public string? leetcodeUsername { get; set; }
        [StringLength(50)]
        public string? githubUsername { get; set; }
        [StringLength(50)]
        public string? codechefUsername { get; set; }
        [StringLength(50)]
        public string? hackerrankUsername { get; set; }
        [StringLength(50)]
        public string? codeforcesUsername { get; set; }
        [StringLength(50)]
        public string? gfgUsername { get; set; }
        [Required]
        public List<string> skills { get; set; } = new List<string>();
        public List<string> interests { get; set; } = new List<string>();
        [Required]
        [StringLength(500)]
        public string description { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<CodingPlatformStats> CodingStats { get; set; } = new List<CodingPlatformStats>();
    }
}
