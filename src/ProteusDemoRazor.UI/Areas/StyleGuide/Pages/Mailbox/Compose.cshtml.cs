using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Mailbox
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Email-Compose", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ComposeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
