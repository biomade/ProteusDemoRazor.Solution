using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Project-Details", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ProjectDetailModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
