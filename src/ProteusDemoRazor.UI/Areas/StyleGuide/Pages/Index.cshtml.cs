using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages
{
    //reported a bug as the error is the same name but in a different area
    //https://github.com/zHaytam/SmartBreadcrumbs/issues/62
    //[Breadcrumb(AreaName = "StyleGuide", Title = "Home")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
