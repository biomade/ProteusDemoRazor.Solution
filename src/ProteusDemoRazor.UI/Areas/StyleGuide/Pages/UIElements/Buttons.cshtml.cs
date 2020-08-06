
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Buttons", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ButtonsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
