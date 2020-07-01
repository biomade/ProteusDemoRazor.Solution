using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages
{
    [DefaultBreadcrumb("Home")]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {           
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
