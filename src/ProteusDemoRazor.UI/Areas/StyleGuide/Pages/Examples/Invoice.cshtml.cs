using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Invoice", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class InvoiceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
