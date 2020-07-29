using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Core.Interfaces.Identity;
using Proteus.Infrastructure.Identity.Stores;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    [Breadcrumb("Delete", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IUserRoleStore _userRoleStore; //use the store as there is no manager
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IUserRoleStore userRoleStore, ILogger<DeleteModel> logger)
        {
            _userRoleStore = (UserRoleStore)userRoleStore;
            _logger = logger;
        }

        [BindProperty]
        public UserRole UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _userRoleStore.FindByIdAsync((int)id);

            if (UserRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _userRoleStore.FindByIdAsync((int)id);

            if (UserRole != null)
            {
                await _userRoleStore.DeleteAsync(UserRole); 
            }

            return RedirectToPage("./Index");
        }
    }
}
