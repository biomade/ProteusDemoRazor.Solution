using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Widgets
{
    [Breadcrumb(AreaName ="StyleGuide", Title = "Widgets", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class WidgetsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
