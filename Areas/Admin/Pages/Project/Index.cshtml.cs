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

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projects != null)
            {
                Project = await _context.Projects.ToListAsync();
            }
        }
    }
}
