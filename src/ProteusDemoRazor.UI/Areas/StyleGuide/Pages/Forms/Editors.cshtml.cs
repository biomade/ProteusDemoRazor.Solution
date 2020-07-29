using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Forms
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Forms-Editors", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class EditorsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
