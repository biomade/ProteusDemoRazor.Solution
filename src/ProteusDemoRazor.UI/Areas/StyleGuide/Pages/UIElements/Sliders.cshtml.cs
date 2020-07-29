using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Sliders", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class SlidersModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
