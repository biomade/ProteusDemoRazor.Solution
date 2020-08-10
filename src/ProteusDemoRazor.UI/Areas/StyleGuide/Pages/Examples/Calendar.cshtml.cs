using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Calendar", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class CalendarModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
