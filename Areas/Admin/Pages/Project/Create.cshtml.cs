using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.ManageProject
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IssueTracker.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        [BindProperty]
        public string? Swimlanes { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Projects == null || Project == null)
            {
                return Page();
            }

            if (!String.IsNullOrEmpty(Swimlanes)) {
                Project.Swimlanes = new List<Swimlane>();

                foreach (Swimlane swimlane in JsonSerializer.Deserialize<List<Swimlane>>(Swimlanes)) {
                    Project.Swimlanes.Add(swimlane);
                }
            }

            // Project.CreatedAt = DateTime.UtcNow;
            // Project.UpdatedAt = DateTime.UtcNow;
            /*Project.UserId = (await _userManager.GetUserAsync(User)).Id;*/
            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
