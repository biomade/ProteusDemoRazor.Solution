using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "eCommerce", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class eCommerceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
