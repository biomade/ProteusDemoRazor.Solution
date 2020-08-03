using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Charts
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Chart JS", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ChartjsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
