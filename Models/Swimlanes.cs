using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Data {
    public class Swimlane {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        // Foreign key to parent project
        public Guid ProjectId { get; set; }
        // Navigation property. Read more at https://learn.microsoft.com/en-us/ef/core/modeling/relationships
        public Project? Project { get; set; }
    }
}