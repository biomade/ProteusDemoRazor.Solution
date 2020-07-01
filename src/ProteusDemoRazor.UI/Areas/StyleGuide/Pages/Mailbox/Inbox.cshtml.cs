using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Mailbox
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Email-Inbox", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class InboxModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
