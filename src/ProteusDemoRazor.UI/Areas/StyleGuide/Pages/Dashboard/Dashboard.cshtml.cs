using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Dashboard
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Dashboard", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
