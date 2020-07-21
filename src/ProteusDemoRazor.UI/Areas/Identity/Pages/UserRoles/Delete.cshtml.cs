using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Areas.Identity.Pages.UserRoles
{
    public class DeleteModel : PageModel
    {
        private readonly Proteus.Infrastructure.Identity.IdentityDbContext _context;

        public DeleteModel(Proteus.Infrastructure.Identity.IdentityDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserRole UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _context.UserRoles
                .Include(u => u.Role)
                .Include(u => u.User).FirstOrDefaultAsync(m => m.Id == id);

            if (UserRole == null)
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

            UserRole = await _context.UserRoles.FindAsync(id);

            if (UserRole != null)
            {
                _context.UserRoles.Remove(UserRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
