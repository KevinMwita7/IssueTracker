using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace IssueTracker.Data {
    public class Project {
        public Guid Id {get; set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get; set;}
        // Foreign keys and navigation properties. Read more at https://learn.microsoft.com/en-us/ef/core/modeling/relationships
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        
        public List<Swimlane>? Swimlanes { get; set; }
    }
}