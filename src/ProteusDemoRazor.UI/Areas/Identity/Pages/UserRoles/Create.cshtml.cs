using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    [Breadcrumb("Create", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly Proteus.Infrastructure.Identity.IdentityDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        

        public CreateModel(Proteus.Infrastructure.Identity.IdentityDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
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
            var exitingUser = _context.UserRoles.FirstOrDefault(ur => ur.RoleId == UserRole.RoleId && ur.UserId == UserRole.UserId);
            if(exitingUser != null)
            {
                string msg = "User already exsists with this Role";
                ModelState.AddModelError(string.Empty, msg );
                _logger.LogWarning( msg);
                GenerateLists(Id, Type);
                return Page();
            }


            UserRole.CreatedDate = System.DateTime.Now;
            _context.UserRoles.Add(UserRole);
            await _context.SaveChangesAsync();

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
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", id);
                    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
                }
                else if (type.ToLower() == "r")
                {
                    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", id);
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
                }
            }
            else
            {
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            }
        }

    }
}
