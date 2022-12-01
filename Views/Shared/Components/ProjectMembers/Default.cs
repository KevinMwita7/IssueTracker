using Microsoft.AspNetCore.Mvc;
using IssueTracker.Data;

namespace IssueTracker.Pages.Components {
    public class ProjectMembersViewComponent : ViewComponent {
        public async Task<IViewComponentResult> InvokeAsync(Project? project) {
            // TODO: load the related members into memory
            // if (project == null || project.Members == null) return View(new List<ApplicationUser>());
            return View(project.Members);
        }
    }
}