
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-General", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class GeneralModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
