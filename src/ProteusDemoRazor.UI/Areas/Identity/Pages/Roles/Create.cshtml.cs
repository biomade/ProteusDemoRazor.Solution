using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(RoleManager<Role> roleManager, ILogger<CreateModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Role.NormalizedName = Role.Name.ToUpper();
            Role.CreatedDate = System.DateTime.Now;
            IdentityResult result =   await _roleManager.CreateAsync(Role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                { 
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogWarning(error.Description);
                }

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
