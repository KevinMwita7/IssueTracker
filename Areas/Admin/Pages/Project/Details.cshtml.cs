using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.ManageProject
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public DetailsModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Project Project { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            else 
            {
                Project = project;
            }
            return Page();
        }
    }
}
