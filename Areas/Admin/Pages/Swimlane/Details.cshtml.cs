using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.Swimlanes
{
    public class DetailsModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public DetailsModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
