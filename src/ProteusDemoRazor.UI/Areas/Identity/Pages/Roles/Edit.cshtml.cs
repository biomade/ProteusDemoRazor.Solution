using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account.Roles;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    [Breadcrumb("Edit", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<EditModel> _logger;

        public EditModel(RoleManager<Role> roleManager, ILogger<EditModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public RoleEditViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return NotFound();
            }
            Input = new RoleEditViewModel();
            Input.Id = role.Id;
            Input.Name = role.Name;
            Input.Description = role.Description;

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
                //Possibly don't allow role name to change 
                //in case it is used for authorization which will mess things up
                var role = await _roleManager.FindByIdAsync(Input.Id.ToString());

                role.Description = Input.Description;
                role.ModifiedDate = System.DateTime.Now;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Profile Updated");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Input.Id))
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
