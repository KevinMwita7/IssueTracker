using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace IssueTracker.Data {
    public class Project : BaseEntity {
        public Guid Id {get; set; }
        public string Name { get; set; }
        // Creator of the project
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        
        public List<Swimlane>? Swimlanes { get; set; }
        public List<ApplicationUser>? Members { get; set; }
    }
}