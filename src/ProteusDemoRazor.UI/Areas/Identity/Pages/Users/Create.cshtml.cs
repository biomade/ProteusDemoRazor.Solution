using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels.Identity.Account.Users;
using Proteus.Core.Constants;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    [Breadcrumb("Create", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreateModel> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IApplicationConfiguration _configuration;

        public CreateModel(UserManager<User> userManager, ILogger<CreateModel> logger, IPasswordHasher<User> passwordHasher, IApplicationConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configuration = configuration; 
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public UserCreateViewModel Input { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Required Fields are Missing");
                return Page();
            }

            var user = new User();
            
            user.CreatedDate = DateTime.Now;
            user.Email = Input.Email;
            user.FirstName = Input.FirstName;
            user.GovPOCEmail = Input.GovPOCEmail;
            user.GovPOCName = Input.GovPOCName;
            user.GovPOCPhoneNumber = Input.GovPOCPhoneNumber;
            user.IsEnabled = Input.IsEnabled;
            if (Input.IsEnabled)
            {
                user.LastLoginDate = System.DateTime.Now;
            }
            user.IsLockedOut = Input.IsLockedOut;
            user.LastName = Input.LastName;
            user.MI = user.MI;        
            user.NormalizedEmail = Input.Email.ToUpper();
            user.NormalizedUserName = Input.UserName.ToUpper();
            user.PhoneNumber = Input.Phone;
            user.UserName = Input.UserName;

           //hash the password and put it back!
           user.PasswordHash = _passwordHasher.HashPassword(user,Input.Password);

            IdentityResult result =  await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogWarning(error.Description);
                }

                return Page();
            }

            //set up user with the visitor Role
            //now set up a default role
            user = await _userManager.FindByNameAsync(user.UserName);

            var roleResult = await _userManager.AddToRoleAsync(user, _configuration.DefaultRole);

            return RedirectToPage("./Index");
        }
    }
}
