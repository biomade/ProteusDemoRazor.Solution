using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Mailbox
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Email-Read", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ReadModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
