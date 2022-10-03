using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.User
{
    public class UserDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDetailsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

      public ApplicationUser SelectedUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                SelectedUser = user;
            }
            return Page();
        }
    }
}
