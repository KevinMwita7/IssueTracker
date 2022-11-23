using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Data {
    public class ApplicationUser : IdentityUser {
        public List<Project>? Projects { get; set; }
    }
}