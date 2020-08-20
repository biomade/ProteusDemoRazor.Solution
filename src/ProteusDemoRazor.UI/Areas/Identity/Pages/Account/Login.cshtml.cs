using System;
using System.Collections.Generic;
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
        private readonly IApplicationConfiguration _configuration;
        private readonly ICertValidationService _validationService;

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public LoginViewModel Input { get; set; }
        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, IApplicationConfiguration configuration, ICertValidationService service)
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

            if (Input.DODAccept == false)
            {
                ModelState.AddModelError(string.Empty, "You MUST accept the DoD statement before you can use this website.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                //grab the uers cert
                X509Certificate2 x509 = HttpContext.Connection.ClientCertificate;
                List<Tuple<string, string>> certInfo = _validationService.GetCertificateInfo(x509);
                var user = await _signInManager.UserManager.FindByNameAsync(certInfo[1].Item2);

                if (user == null)
                {
                    this.Input.DODAccept = false;
                    ModelState.Clear();
                    TempData["ViewData"] = "No account found, registration is required.";
                    _logger.LogWarning("No account found, registration required.");
                    return Redirect("./Register");
                }
                else if (!user.IsEnabled)
                {
                    this.Input.DODAccept = false;
                    ModelState.Clear();
                    return RedirectToPage("./AccountDisabled");
                }
                else if (user.IsLockedOut)
                {
                    this.Input.DODAccept = false;
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
                        await _signInManager.UserManager.UpdateAsync(user);
                        this.Input.DODAccept = false;
                        ModelState.Clear();
                        _logger.LogWarning(string.Format("User Account has been Disabled due to lack of use, it has been more than {0} since your last login: {1}.", _configuration.MaxDaysBetweenLogins, user.UserName));
                        return RedirectToPage("./AccountLocked");
                    }

                    //do they have a session already??
                    if(user.UserOnLine == true )
                    {
                        //if if was yesterday log them out so they can log in
                        if(user.LastLoginDate < DateTime.Today)
                        {                           
                            user.UserOnLine = false;
                            await _signInManager.UserManager.UpdateAsync(user);
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

                    //verify the EDI matches what is on file
                    if (user.EDI != certInfo[2].Item2)
                    {
                        //cert 
                        _logger.LogWarning(string.Format("EDI on CAC does not match Database:{0}",  user.UserName ));
                        ModelState.AddModelError(string.Empty, "There is an issue with your CAC login, contact the Administrator and let them know.");
                        return Page();
                    }

                    //LOG THEM IN!!
                    //create the CAC as a claim 
                    //note: roles are added as claims due to the configuration statup.cs file
                    //add cac edit info to the claims                  
                    var identityClaim = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    var customClaims = new[]
                    {
                         new Claim(ClaimTypes.NameIdentifier,certInfo[0].Item2, ClaimValueTypes.String,"DOD-CAC"),
                         new Claim(ClaimTypes.GivenName,certInfo[1].Item2, ClaimValueTypes.String)
                    };                    
                   
                    await _signInManager.SignInWithClaimsAsync(user, isPersistent: false , customClaims);

                    //now set the login date time
                    user.LastLoginDate = DateTime.Now;
                    user.UserOnLine = true;
                    await _signInManager.UserManager.UpdateAsync(user);
                    _logger.LogInformation(String.Format("User logged in: {0}.", user.UserName));
                    return LocalRedirect(returnUrl);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}

