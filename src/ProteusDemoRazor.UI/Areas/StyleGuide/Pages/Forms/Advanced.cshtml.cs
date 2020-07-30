using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Forms
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Forms-Advanced", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class AdvancedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
