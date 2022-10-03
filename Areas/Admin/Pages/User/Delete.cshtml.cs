using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Data;


namespace IssueTracker.Areas.Admin.Pages.User
{
    public class DeleteUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);


            if (user != null)
            {
                // Don't allow admin to delete themselves. Can only be done by
                // another admin. This is to prevent the site from remaining without
                // an admin. Other approach would be counting admins in DB and if count
                // exceeds 1 proceed with deletion
                if(user.Id != loggedInUser.Id) await _userManager.DeleteAsync(user);
                else {
                    SelectedUser = user;
                    ModelState.AddModelError(string.Empty, "You can't delete your own account. It has to be done by another admin!");
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
