using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Core.Interfaces.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    [Breadcrumb("Create", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {

        private readonly IUserRoleStore _userRoleStore; //use the store as there is no manager
        private readonly ILogger<CreateModel> _logger;
        

        public CreateModel(IUserRoleStore userRoleStore, ILogger<CreateModel> logger)
        {
            _userRoleStore = userRoleStore;
            _logger = logger;
        }

        public IActionResult OnGet(int? id, string type)
        {
            GenerateLists(id, type);

            return Page();
        }

        [BindProperty]
        public UserRole UserRole { get; set; }

        private int? Id { get; set; }
        private string Type { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GenerateLists(Id, Type);
                return Page();
            }

            //check to see if user is already in the role
            var exitingUser = _userRoleStore.FindByIdAsync(UserRole.RoleId,UserRole.UserId);

            if (exitingUser != null)
            {
                string msg = "User already exsists with this Role";
                ModelState.AddModelError(string.Empty, msg );
                _logger.LogWarning( msg);
                GenerateLists(Id, Type);
                return Page();
            }

            UserRole.CreatedDate = System.DateTime.Now;
            var result = await _userRoleStore.DeleteAsync(UserRole);
           

            return RedirectToPage("./Index");
        }

        private void GenerateLists(int? id, string type)
        {
            if (id != null && !String.IsNullOrEmpty(type))
            {
                Id = id;
                Type = type;

                //if id and type set the value of the items selected
                if (type.ToLower() == "u")
                {
                    ViewData["UserId"] = new SelectList(_userRoleStore.GetUsersAsync().Result, "Id", "UserName", id);
                    ViewData["RoleId"] = new SelectList(_userRoleStore.GetRolesAsync().Result, "Id", "Name");
                }
                else if (type.ToLower() == "r")
                {
                    ViewData["UserId"] = new SelectList(_userRoleStore.GetUsersAsync().Result, "Id", "UserName");
                    ViewData["RoleId"] = new SelectList(_userRoleStore.GetRolesAsync().Result, "Id", "Name", id);
                    
                }
            }
            else
            {
                ViewData["UserId"] = new SelectList(_userRoleStore.GetUsersAsync().Result , "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_userRoleStore.GetRolesAsync().Result, "Id", "Name");
            }
        }

    }
}
