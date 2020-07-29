using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Contacts", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class ContactsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
