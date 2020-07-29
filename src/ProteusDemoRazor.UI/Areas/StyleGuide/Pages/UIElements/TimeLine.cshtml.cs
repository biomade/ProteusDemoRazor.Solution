using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-TimeLine", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class TimeLineModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
