using IssueTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Areas.Api {    
    [Area("Api")]
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class ProjectController : Controller {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();

            var Project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            return ViewComponent("ProjectDetails", new {
                project = Project
            });
        }

        [HttpGet]
        public async Task<IActionResult> Members(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();

            var Project = await _context.Projects
                .Include(p => p.Members)
                .FirstOrDefaultAsync(p => p.Id == id);

            return ViewComponent("ProjectMembers", new {
                project = Project
            });
        }

        [HttpGet]
        public async Task<IActionResult> Swimlanes(Guid id) {
            if (id == null || _context.Projects == null) return NotFound();
            
            var Project = await _context.Projects
                .Include(p => p.Swimlanes)
                .FirstOrDefaultAsync(p => p.Id == id);

            return ViewComponent("ProjectSwimlanes", new {
                project = Project
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Swimlane(Guid projectId, Guid swimlaneId) {
            Console.WriteLine("---------------------------");
            Console.WriteLine(projectId);
            Console.WriteLine(swimlaneId);
            Console.WriteLine("---------------------------");
            if (projectId == null || swimlaneId == null || _context.Swimlane == null) return NotFound();

            var Swimlane = _context.Swimlane.Where(s => s.Id == swimlaneId).Where(s => s.ProjectId == projectId).First();

            if (Swimlane != null) {
                _context.Swimlane.Remove(Swimlane);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        public IActionResult AddMember() {
            Console.WriteLine("-----------------------");
            Console.WriteLine("add member");
            Console.WriteLine("-----------------------");
            return NotFound();
        }
    }
}