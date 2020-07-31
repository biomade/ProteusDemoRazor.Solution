using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Core.Entities.Identity;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class Login2Model : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly IApplicationConfiguration _configuration;

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Login2ViewModel Input { get; set; }
        public Login2Model(SignInManager<User> signInManager, ILogger<LoginModel> logger, IApplicationConfiguration configuration)
        {
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);


            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            string why = string.Empty;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true and implement IUserLockoutStore
                Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
                var user = await _signInManager.UserManager.FindByNameAsync(Input.UserName);
                if (user == null)
                {
                    result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;
                }
                else if (!user.IsEnabled)
                {
                    result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;
                  
                }
                else if (DateTime.Now.Subtract(user.LastLoginDate).Days >= _configuration.MaxDaysBetweenLogins)
                {
                    //if they have not logged in for x number of days
                    //disable the account!
                    user.IsEnabled = false;
                    await _signInManager.UserManager.UpdateAsync(user);
                    result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;
                    why = "AccountDisabledDueToLackOfUse";
                }
                else
                {
                    //check the password is correct
                    bool validPassword = await _signInManager.UserManager.CheckPasswordAsync(user, Input.Password);
                    if (validPassword)
                    {
                        ////get the roles
                        //List<string> roleNames = (List<string>)await _signInManager.UserManager.GetRolesAsync(user) ;
                        //var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                        //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
                        //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                        //foreach (var roleName in roleNames)
                        //{
                        //    new Claim(ClaimTypes.Role, roleName);
                        //    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
                        //    result = Microsoft.AspNetCore.Identity.SignInResult.Success;
                        //}
                        //allow the user in!

                        result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, lockoutOnFailure: false);
                    }

                }

                if (result.Succeeded)
                {
                    //now set the login time
                    user.LastLoginDate = DateTime.Now;
                    await _signInManager.UserManager.UpdateAsync(user);
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else if (result.IsNotAllowed)
                {
                    if (why == "AccountDisabledDueToLackOfUse")
                    {
                        ModelState.AddModelError(string.Empty, "User Account has been Disabled due to lack of use.");
                        _logger.LogWarning("User Account has not been enabled or it has been Disabled due to lack of use");
                        return Page();
                    }

                    ModelState.AddModelError(string.Empty, "User Account has not been Enabled.");
                    _logger.LogWarning("User Account has not been Enabled.");
                    return RedirectToPage("./AccountDisabled");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

