using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    [Breadcrumb("Delete", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(RoleManager<Role> roleManager, ILogger<DeleteModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _roleManager.FindByIdAsync(id.ToString());

            if (Role == null)
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

            Role = await _roleManager.FindByIdAsync(id.ToString());

            if (Role != null)
            {
                //if there a users in the role, don't delete
                if (Role.UserRoles.Count >= 0)
                {
                    ModelState.AddModelError(string.Empty, "Users assigned to this role must be removed before you can delete the Role");
                    _logger.LogWarning("Delete Role: Users assigned to this role must be removed before you can delete the Role");
                    return Page();
                }
              var result =   await _roleManager.DeleteAsync(Role);
            }

            return RedirectToPage("./Index");
        }
    }
}
