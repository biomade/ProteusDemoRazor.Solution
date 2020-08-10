
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Project-Edit", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ProjectEditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
