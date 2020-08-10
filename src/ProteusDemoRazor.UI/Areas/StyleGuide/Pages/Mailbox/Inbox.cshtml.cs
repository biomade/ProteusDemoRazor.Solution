using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Mailbox
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Email-Inbox", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class InboxModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
