using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Dashboard
{
    //[Breadcrumb(AreaName = "StyleGuide", Title = "Dashboard", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
