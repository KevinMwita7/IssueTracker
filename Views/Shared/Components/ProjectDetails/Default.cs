using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

namespace IssueTracker.Pages.Components {
    public class ProjectDetailsViewComponent : ViewComponent {
        private readonly ApplicationDbContext _context;

        public ProjectDetailsViewComponent(ApplicationDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync(Project? project) {
            // if (id == null || _context.Projects == null) {
            //     return View();
            // }

            // var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            // if (project == null) {
            //     return View();
            // }
            return View(project);
        }
    }
}