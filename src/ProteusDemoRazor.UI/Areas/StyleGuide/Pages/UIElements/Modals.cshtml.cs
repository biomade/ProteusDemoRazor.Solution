using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Modals", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ModalsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
