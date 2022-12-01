using IssueTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Areas.Api {    
    [Area("Api")]
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class ProjectController : Controller {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Details(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();

            var Project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            return ViewComponent("ProjectDetails", new {
                project = Project
            });
        }

        public async Task<IActionResult> Members(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();

            var Project = await _context.Projects
                .Include(p => p.Members)
                .FirstOrDefaultAsync(p => p.Id == id);

            return ViewComponent("ProjectMembers", new {
                project = Project
            });
        }

        public async Task<IActionResult> Swimlanes(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();
            
            var Project = await _context.Projects
                .Include(p => p.Swimlanes)
                .FirstOrDefaultAsync(p => p.Id == id);

            return ViewComponent("ProjectSwimlanes", new {
                project = Project
            });
        }

        public IActionResult AddMember() {
            Console.WriteLine("-----------------------");
            Console.WriteLine("add member");
            Console.WriteLine("-----------------------");
            return NotFound();
        }
    }
}