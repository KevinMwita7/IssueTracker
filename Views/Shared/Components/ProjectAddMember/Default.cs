using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Views.Components {
    public class ProjectAddMemberViewComponent : ViewComponent {
        public IViewComponentResult Invoke(List<SelectListItem> users) {
            return View(users);
        }
    }
}