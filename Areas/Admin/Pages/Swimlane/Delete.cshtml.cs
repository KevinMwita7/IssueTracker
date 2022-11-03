using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.Swimlanes
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public DeleteModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Swimlane Swimlane { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Swimlane == null)
            {
                return NotFound();
            }

            var swimlane = await _context.Swimlane.FirstOrDefaultAsync(m => m.Id == id);

            if (swimlane == null)
            {
                return NotFound();
            }
            else 
            {
                Swimlane = swimlane;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Swimlane == null)
            {
                return NotFound();
            }
            var swimlane = await _context.Swimlane.FindAsync(id);

            if (swimlane != null)
            {
                Swimlane = swimlane;
                _context.Swimlane.Remove(Swimlane);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
