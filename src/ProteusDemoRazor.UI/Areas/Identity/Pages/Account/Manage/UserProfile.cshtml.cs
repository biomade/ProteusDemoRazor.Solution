using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.Mapper;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Application.ViewModels.Identity.Account.Manage;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    [Breadcrumb("User Profile")]
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public UserProfileViewModel Input { get; set; } = new UserProfileViewModel();

        public UserProfileModel(UserManager<User> userManager, ILogger<UserProfileModel> logger, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }
        public async Task<IActionResult> OnGet()
        {
            //now get the user
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            //automapper converts it
            Input.Email = user.Email;
            Input.FirstName = user.FirstName;
            Input.GovPOCEmail = user.GovPOCEmail;
            Input.GovPOCName = user.GovPOCName;
            Input.GovPOCPhoneNumber = user.GovPOCPhoneNumber;
            Input.Id = user.Id;
            Input.LastName = user.LastName;
            Input.MI = user.MI;
            Input.PasswordHash = user.PasswordHash;
            Input.Phone = user.PhoneNumber;
            Input.UserName = user.UserName;
            Input.EDI = user.EDI;

            //Input =  ObjectMapper.Mapper.Map<UserProfileViewModel>(user);           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //get user and update the profile fields
            var user = await _userManager.FindByIdAsync(Input.Id.ToString());

            user.FirstName = Input.FirstName;
            user.MI = Input.MI;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.Phone;
            user.GovPOCEmail = Input.GovPOCEmail;
            user.GovPOCName = Input.GovPOCName;
            user.GovPOCPhoneNumber = Input.GovPOCPhoneNumber;
            user.EDI = Input.EDI;

            if (String.IsNullOrEmpty(Input.Email))
            {
                ModelState.AddModelError(string.Empty, "Email can not be empty");
                _logger.LogWarning("Email can not be empty");
                return Page();
            }
            else
            {
                user.Email = Input.Email;
            }

            user.ModifiedDate = System.DateTime.Now;
            user.PasswordHash = Input.PasswordHash;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Profile Updated");
            }

            return Page();
        }
    }
}
