using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Projects", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ProjectsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
