using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Documentation
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Documentation", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class DocumentationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
