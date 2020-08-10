using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Forms
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Forms", FromPage = typeof(StyleGuide.Pages.IndexStyleModel))]
    public class GeneralModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
