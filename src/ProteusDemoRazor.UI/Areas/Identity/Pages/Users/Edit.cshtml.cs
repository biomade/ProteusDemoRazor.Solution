using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Constants;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    [Breadcrumb("Edit", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public EditModel(UserManager<User> userManager, ILogger<EditModel> logger, IPasswordHasher<User> passwordHasher)
        {
             _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public User UserViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel = await _userManager.FindByIdAsync(id.ToString());
            
            if (UserViewModel == null)
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
                UserViewModel.ModifiedDate = System.DateTime.Now;
                UserViewModel.NormalizedEmail = UserViewModel.Email.ToUpper();               ;
                await _userManager.UpdateAsync(UserViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserViewModel.Id))
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

        private bool UserExists(int id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;

            return (user != null) ? true : false;
        }
    }
}
