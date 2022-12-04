using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Views.Components {
    public class ProjectAddSwimlanesViewComponent : ViewComponent {
        public IViewComponentResult Invoke(List<SelectListItem> projects) {
            return View(projects);
        }
    }
}