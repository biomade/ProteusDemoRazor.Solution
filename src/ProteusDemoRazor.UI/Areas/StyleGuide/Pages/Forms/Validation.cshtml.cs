using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Forms
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Forms-Validation", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class ValidationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
