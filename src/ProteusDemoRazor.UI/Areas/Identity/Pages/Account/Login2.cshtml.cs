using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly IApplicationConfiguration _configuration;

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Login2ViewModel Input { get; set; }
        public Login2Model(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<Login2Model> logger, IApplicationConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
             _logger = logger;
            _configuration = configuration;
        }

        public IActionResult OnGet(string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            ReturnUrl = returnUrl;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            string why = string.Empty;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true and implement IUserLockoutStore
                //Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
                var user = await _userManager.FindByNameAsync(Input.UserName);
                if (user == null)
                {
                    ModelState.Clear();
                    //this is passed to the Register page!
                    TempData["ViewData"] = "No account found, registration is required.";
                    _logger.LogWarning("No account found, registration required.");
                    return RedirectToPage("./Register");

                    //result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;
                }
                else if (!user.IsEnabled)
                {
                    ModelState.Clear();
                    return RedirectToPage("./AccountDisabled");
                }
                else if (user.IsLockedOut)
                {
                    ModelState.Clear();
                    _logger.LogWarning(string.Format("User Account has been Disabled due to lack of use, it has been more than {0} since your last login: {1}.", _configuration.MaxDaysBetweenLogins, user.UserName));
                    return RedirectToPage("./AccountLocked");
                }
                else
                {
                    //we have an account but SHOULD they be locked out
                    if (DateTime.Now.Subtract(user.LastLoginDate).Days >= _configuration.MaxDaysBetweenLogins)
                    {
                        //if they have not logged in for x number of days
                        //disable the account!
                        user.IsEnabled = false;
                        await _userManager.UpdateAsync(user);
                        ModelState.Clear();
                        _logger.LogWarning(string.Format("User Account has been Disabled due to lack of use, it has been more than {0} since your last login: {1}.", _configuration.MaxDaysBetweenLogins, user.UserName));
                        return RedirectToPage("./AccountLocked");
                    }
                    //do they have a session already??
                    if (user.UserOnLine == true)
                    {
                        //if if was yesterday log them out so they can log in
                        if (user.LastLoginDate < DateTime.Today)
                        {
                            user.UserOnLine = false;
                            await _userManager.UpdateAsync(user);
                            await _signInManager.SignOutAsync();
                        }

                        if (user.LastLoginDate.Date == DateTime.Today)
                        {
                            //anytime today is not allowed
                            _logger.LogWarning(string.Format("Second Sesson:{0}", user.UserName));
                            ModelState.AddModelError(string.Empty, "It applears that you are attempting to start another session, that is prohibited.");
                            return Page();
                        }

                    }

                    //check the password is correct
                    bool validPassword = await _userManager.CheckPasswordAsync(user, Input.Password);
                    if (validPassword)
                    {
                        //roles are added as claims as part of a service in the setup
                        //allow the user in!
                        var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            //now set the login time
                            user.LastLoginDate = DateTime.Now;
                            user.UserOnLine = true;
                            await _userManager.UpdateAsync(user);
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrl);
                        }
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}