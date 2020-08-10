using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Invoice", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class InvoiceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
