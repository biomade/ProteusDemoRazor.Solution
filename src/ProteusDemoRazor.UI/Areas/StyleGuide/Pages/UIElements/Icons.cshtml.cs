
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Icons", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class IconsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
