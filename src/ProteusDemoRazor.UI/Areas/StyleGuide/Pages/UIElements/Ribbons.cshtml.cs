using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Ribbons", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class RibbonsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
