using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Data {
    public class Swimlane : BaseEntity {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        // Foreign key to parent project
        public Guid ProjectId { get; set; }
        // Navigation property. Read more at https://learn.microsoft.com/en-us/ef/core/modeling/relationships
        public Project? Project { get; set; }
        public List<ProjectTask>? Tasks { get; set; }
    }
}