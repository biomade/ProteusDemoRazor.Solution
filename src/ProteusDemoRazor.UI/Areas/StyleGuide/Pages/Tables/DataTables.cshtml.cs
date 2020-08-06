
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Tables
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Tables-DataTables", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class DataTablesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
