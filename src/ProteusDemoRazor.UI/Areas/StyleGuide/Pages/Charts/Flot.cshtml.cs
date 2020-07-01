using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Charts
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Flot Chart", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class FlotModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
