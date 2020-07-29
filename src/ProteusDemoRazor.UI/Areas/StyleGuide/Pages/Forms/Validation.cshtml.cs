using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Forms
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Forms-Validation", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ValidationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
