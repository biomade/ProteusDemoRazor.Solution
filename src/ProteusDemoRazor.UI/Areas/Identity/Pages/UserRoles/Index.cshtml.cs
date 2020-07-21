using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    [Breadcrumb("UserRoles")]
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly Proteus.Infrastructure.Identity.IdentityDbContext _context;

        public IndexModel(Proteus.Infrastructure.Identity.IdentityDbContext context)
        {
            _context = context;
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
                UserRole = await _context.UserRoles
                .Include(u => u.Role)
                .Include(u => u.User).ToListAsync();
            }
            else if (type.ToLower() =="r")
            {
                UserRole = await _context.UserRoles
               .Include(u => u.Role)
               .Include(u => u.User).Where(ur=>ur.RoleId == id).ToListAsync();
            }
            else if (type.ToLower() == "u")
            {
                UserRole = await _context.UserRoles
               .Include(u => u.Role)
               .Include(u => u.User).Where(ur => ur.UserId == id).ToListAsync();
            }
        }
    }
}
