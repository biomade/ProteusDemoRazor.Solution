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

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreateModel> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateModel(UserManager<User> userManager, ILogger<CreateModel> logger, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User.NormalizedUserName = User.UserName.ToUpper();
            User.NormalizedEmail = User.Email.ToUpper();
            User.PasswordHash = _passwordHasher.HashPassword(User, User.PasswordHash);

            if (User.IsEnabled)
            {
                User.LastLoginDate = System.DateTime.Now;
            }
            User.CreatedDate = System.DateTime.Now;
            //hash the password entered


            IdentityResult result =  await _userManager.CreateAsync(User);
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
