using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Data {
    public class ApplicationUser : IdentityUser {
        List<Project> Projects { get; set; }
    }
}