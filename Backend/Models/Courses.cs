using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Courses
    {
        [Column("courseId")]
        public int Id { get; set; }
        [Required]
        public string courseName { get; set; } = string.Empty;
        [Required]
        public string courseDescription { get; set; } = string.Empty;
        [Required]
        public string courseImageUrl { get; set; } = string.Empty;
        public int courseDuration { get; set; }
        [Required]
        public string courseLevel { get; set; } = string.Empty;
        [Required]
        public string courseLink { get; set; } = string.Empty;
        [Column("courseCreatedAt")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
