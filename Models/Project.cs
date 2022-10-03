using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Data {
    public class Project {
        public Guid Id {get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt {get; set;}
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}