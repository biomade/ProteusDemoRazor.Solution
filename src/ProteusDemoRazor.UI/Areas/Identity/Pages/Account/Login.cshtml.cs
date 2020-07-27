using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ICertValidationService _validationService;

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        
        [BindProperty]
        public LoginViewModel Input { get; set; }
        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, IConfiguration configuration, ICertValidationService service)
        {
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _validationService = service;
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

            if (Input.DODAccept == false)
            {
                ModelState.AddModelError(string.Empty, "You MUST accept the DoD statement before you can use this website.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                //grab the uers cert
                X509Certificate2 x509 = HttpContext.Connection.ClientCertificate;
                List<Tuple<string, string>> certInfo =  _validationService.GetCertificateInfo(x509);

                var user = await _signInManager.UserManager.FindByNameAsync("Admin");

                Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Failed;

                //var user = await _signInManager.UserManager.

                var blah =  _signInManager.SignInAsync(user, false);

                return LocalRedirect(returnUrl);

                /*
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true and implement IUserLockoutStore
               
                var user = await _signInManager.UserManager.FindByNameAsync(Input.UserName);
                if(user == null)
                {
                    result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;
                }
                else if (!user.IsEnabled)
                {
                    result = Microsoft.AspNetCore.Identity.SignInResult.NotAllowed;                    
                }
                else if (DateTime.Now.Subtract(user.LastLoginDate).Days >= Convert.ToInt32(_configuration["AppSettings:MaxDaysBetweenLogins"]))
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
                   */
            }
           
            /*
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
                    if(why == "AccountDisabledDueToLackOfUse")
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
            */

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
