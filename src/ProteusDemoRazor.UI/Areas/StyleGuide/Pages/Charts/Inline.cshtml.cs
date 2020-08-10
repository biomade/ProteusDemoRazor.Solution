using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Charts
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Inline Chart", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class InlineModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
