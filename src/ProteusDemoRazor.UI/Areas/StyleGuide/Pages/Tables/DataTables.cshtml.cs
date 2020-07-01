using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Tables
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Tables-DataTables", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class DataTablesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
