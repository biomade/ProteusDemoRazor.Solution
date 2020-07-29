
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Profile", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ProfileModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
