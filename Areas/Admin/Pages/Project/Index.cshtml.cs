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
    public class IndexModel : PageModel
    {
        private readonly IssueTracker.Data.ApplicationDbContext _context;

        public IndexModel(IssueTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get;set; } = default!;
        public Project Project { get; set; }

        public async Task OnGetAsync(Guid? id)
        {
            if (_context.Projects != null) {
                if (id != null) {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine(id);
                    Console.WriteLine("--------------------------");
                    Project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                } else {
                    Projects = await _context.Projects
                    .Include(p => p.Creator).ToListAsync();
                }
            }
        }
    }
}
