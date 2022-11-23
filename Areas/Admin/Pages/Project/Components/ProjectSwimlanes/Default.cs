using Microsoft.AspNetCore.Mvc;
using IssueTracker.Data;

namespace IssueTracker.Pages.Components {
    public class ProjectSwimlanesViewComponent : ViewComponent {
            public async Task<IViewComponentResult> InvokeAsync(Project? project) {
            return View(project.Swimlanes);
        }
    }
}