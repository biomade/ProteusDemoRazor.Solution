using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Documentation
{
    //[Breadcrumb(AreaName = "StyleGuide", Title = "Documentation", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
