using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Tables
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Tables-Simple", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class SimpleModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
