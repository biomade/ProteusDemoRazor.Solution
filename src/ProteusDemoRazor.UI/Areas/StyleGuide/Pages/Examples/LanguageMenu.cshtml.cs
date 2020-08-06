using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Language Menu", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class LanguageMenuModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
