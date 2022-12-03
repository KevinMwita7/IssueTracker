using IssueTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace IssueTracker.Areas.Api {    
    [Area("Api")]
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class ProjectController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public class AddMemberFormModel {
            public Guid UserId { get; set; }
            public Guid ProjectId { get; set; }
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

        [HttpGet]
        public IActionResult GetAddMemberForm() {
            return ViewComponent("ProjectAddMember", new {
                users = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName").ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember([FromForm] AddMemberFormModel body) {
            if (body.UserId == null || body.ProjectId == null || _context.Projects == null) {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(body.UserId.ToString());
            var project = await _context.Projects
                .Include(p => p.Members)
                .FirstOrDefaultAsync(p => p.Id == body.ProjectId);

            if (user == null || project == null) {
                return NotFound();
            }

            project.Members.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}