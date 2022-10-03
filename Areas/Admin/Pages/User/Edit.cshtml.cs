using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Data;

namespace IssueTracker.Areas.Admin.Pages.User
{
    [Authorize(Roles = "Administrator")]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EditUserModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public ApplicationUser SelectedUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user =  await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            SelectedUser = user;
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

            var UpdatedUser = await _userManager.FindByIdAsync(SelectedUser.Id);

            try
            {
                UpdatedUser.UserName = SelectedUser.UserName;
                UpdatedUser.Email = SelectedUser.Email;
                UpdatedUser.PhoneNumber = SelectedUser.PhoneNumber;
                await _userManager.UpdateAsync(UpdatedUser);
            }
            catch (Exception)
            {
                if (UpdatedUser == null)
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
    }
}
