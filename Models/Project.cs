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
        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}