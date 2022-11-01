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
    public class IndexModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public IndexModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Swimlane> Swimlane { get;set; } = default!;

        public async Task OnGetAsync(Guid? ProjectId)
        {
            Console.WriteLine(ProjectId);
            if (_context.Swimlane != null)
            {
                var query = from s in _context.Swimlane select s;

                if (ProjectId != null) {
                    query = query.Where(s => s.ProjectId.Equals(ProjectId));
                }

                query = query.Include(s => s.Project);

                Swimlane = await query.ToListAsync();
            }
        }
    }
}
