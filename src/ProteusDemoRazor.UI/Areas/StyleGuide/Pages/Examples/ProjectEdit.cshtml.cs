using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Project-Edit", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ProjectEditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
