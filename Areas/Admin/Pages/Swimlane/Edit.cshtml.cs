using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.Swimlanes
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public EditModel(IssueTracker.Data.ApplicationDbContext context)
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

            var swimlane =  await _context.Swimlane.FirstOrDefaultAsync(m => m.Id == id);
            if (swimlane == null)
            {
                return NotFound();
            }
            Swimlane = swimlane;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry(Swimlane).Property(s => s.Name).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SwimlaneExists(Swimlane.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SwimlaneExists(Guid id)
        {
          return (_context.Swimlane?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
