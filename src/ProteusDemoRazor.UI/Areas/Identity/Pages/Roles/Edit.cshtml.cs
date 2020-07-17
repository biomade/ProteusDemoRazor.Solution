using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateModel> _logger;

        public EditModel(RoleManager<Role> roleManager, ILogger<CreateModel> logger)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _roleManager.UpdateAsync(Role);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Role.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoleExists(int id)
        {
            var role= _roleManager.FindByIdAsync(id.ToString()).Result;
            
            return (role != null) ? true: false;
        }
    }
}
