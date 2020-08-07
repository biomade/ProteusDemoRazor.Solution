using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Core.Interfaces.Identity;
using Proteus.Infrastructure.Identity;
using Proteus.Infrastructure.Identity.Stores;
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

        public IList<UserRole> Inputs { get; set; }

        public string UserRoleType { get; set; }
        public int? Id { get; set; }

        public async Task OnGetAsync(int? id, string type)
        {
            UserRoleType = type;
            Id = id;

            if (id == null)
            {
                Inputs = await _userRoleStore.GetUserRolesAsync();
            }
            else if (type.ToLower() =="r")
            {
                Inputs = await _userRoleStore.GetUserRolesForRoleAsync((int)id);
            }
            else if (type.ToLower() == "u")
            {
                Inputs = await _userRoleStore.GetUserRolesForUserAsync((int)id);
            }
        }
    }
}
