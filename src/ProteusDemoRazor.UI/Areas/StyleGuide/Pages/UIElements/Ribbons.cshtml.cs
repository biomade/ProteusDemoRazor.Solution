using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.UIElements
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "UI-Ribbons", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class RibbonsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
