using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Opportunities
    {
        [Column("opportunityId")]
        public int Id { get; set; }
        [Required]
        public string opportunityName { get; set; } = string.Empty;
        [Required]
        public string opportunityCompany { get; set; } = string.Empty;
        [Required]
        public string opportunityDescription { get; set; } = string.Empty;
        [Required]
        public string opportunityLink { get; set; } = string.Empty;
        [Required]
        public string opportunityImageUrl { get; set; } = string.Empty;
        [Column("opportunityCreatedAt")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime opportunityDeadline { get; set; }
    }
}


