using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Areas.Admin.Pages.User {
    [Authorize(Roles = "Administrator")]
    public class ListUserModel : PageModel {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ListUserModel> _logger;

        public ListUserModel(ILogger<ListUserModel> logger, UserManager<IdentityUser> userManager) {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }
        public IList<IdentityUser> Users { get; set; } = default!;

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            Users = _userManager.Users.ToList();

            returnUrl ??= Url.Content("~/");

            ReturnUrl = returnUrl;
        }
    }
}