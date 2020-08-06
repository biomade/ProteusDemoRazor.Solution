using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages
{
    //reported a bug as the error is the same name but in a different area
    //https://github.com/zHaytam/SmartBreadcrumbs/issues/62
    [Breadcrumb(AreaName = "StyleGuide", Title = "Style Guide Home")]
    public class IndexStyleModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
