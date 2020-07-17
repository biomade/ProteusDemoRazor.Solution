using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateModel> _logger;

        public DeleteModel(RoleManager<Role> roleManager, ILogger<CreateModel> logger)
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
              var result =   await _roleManager.DeleteAsync(Role);
            }

            return RedirectToPage("./Index");
        }
    }
}
