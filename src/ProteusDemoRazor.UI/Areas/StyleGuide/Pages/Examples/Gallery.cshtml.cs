using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.StyleGuide.Pages.Examples
{
    [Breadcrumb(AreaName = "StyleGuide", Title = "Gallery", FromPage = typeof(StyleGuide.Pages.IndexModel))]
    public class GalleryModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
