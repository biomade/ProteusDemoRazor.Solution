using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-NavBars", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class NavBarModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
