using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.Swimlanes
{
    public class CreateModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public CreateModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Swimlane Swimlane { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Swimlane == null || Swimlane == null)
            {
                return Page();
            }

            _context.Swimlane.Add(Swimlane);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
