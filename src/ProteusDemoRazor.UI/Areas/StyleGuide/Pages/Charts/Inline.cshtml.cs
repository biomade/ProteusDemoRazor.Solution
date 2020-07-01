using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Charts
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Inline Chart", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class InlineModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
