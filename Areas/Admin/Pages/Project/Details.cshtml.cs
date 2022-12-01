using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssueTracker.Data;
using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Areas.Admin.Pages.ManageProject
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(IssueTracker.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    
        public Project Project { get; set; } = default!; 
        
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Required]
            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            var project = await LoadProject(id);
            if (project == null) return NotFound();
            Project = project;
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");
            return Page();
        }

        public async Task<IActionResult> OnPostAddMember(Guid? id) {
            var project = await LoadProject(id);
            if (project == null) return NotFound();
            Project = project;
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");

            if (ModelState.IsValid) {
                var user = await _userManager.FindByIdAsync(Input.UserId);
                // Don't add user to context if they already exist in order to avoid
                // duplicates from being displayed in the members section
                if (user != null && !project.Members.Exists(m => m.Id == user.Id)) {
                    project.Members.Add(user);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }

        private async Task<Project> LoadProject(Guid? id) {
            if (id == null || _context.Projects == null) {
                return null;
            }
            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);

            return project;
        }
    }
}
