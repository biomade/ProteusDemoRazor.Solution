using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Core.Interfaces.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    [Breadcrumb("UserRoles")]
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IUserRoleStore _userRoleStore; //use the store as there is no manager
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IUserRoleStore userRoleStore, ILogger<IndexModel> logger)
        {
            _userRoleStore = userRoleStore;
            _logger = logger;
        }

        public IList<UserRole> UserRole { get; set; }

        public string UserRoleType { get; set; }
        public int? Id { get; set; }

        public async Task OnGetAsync(int? id, string type)
        {
            UserRoleType = type;
            Id = id;

            if (id == null)
            {
                UserRole = await _userRoleStore.GetUserRolesAsync();
            }
            else if (type.ToLower() =="r")
            {
                UserRole = await _userRoleStore.GetUserRolesForRoleAsync((int)id);
            }
            else if (type.ToLower() == "u")
            {
                UserRole = await _userRoleStore.GetUserRolesForUserAsync((int)id);
            }
        }
    }
}
