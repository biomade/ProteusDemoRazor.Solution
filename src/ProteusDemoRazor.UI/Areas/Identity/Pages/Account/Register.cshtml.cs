using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Core.Entities.Identity;
using Proteus.Application.Interfaces;
using System.Security.Cryptography.X509Certificates;
using Proteus.Core.Constants;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ICertValidationService _certValidationService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public RegisterModel(
            UserManager<User> userManager,
            ICertValidationService certValidationService,
            ILogger<RegisterModel> logger, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _userManager = userManager;
            _certValidationService = certValidationService;
            _logger = logger;
            _configuration = configuration;
            Input = new RegisterViewModel();
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public RegisterViewModel Input { get; set; }
        public void OnGet(string returnUrl = null)
        {
            if (TempData["ViewData"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["ViewData"].ToString());
            }

            //get the user name and edi
            X509Certificate2 x509 = HttpContext.Connection.ClientCertificate;
            List<Tuple<string, string>> certInfo = _certValidationService.GetCertificateInfo(x509);
            Input.EDI = certInfo[2].Item2;
            Input.Name = certInfo[1].Item2;


            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //get the edi value and user Name
                var user = new User {
                    UserName = Input.Name,
                    Email = Input.Email,
                    PhoneNumber = Input.Phone,
                    GovPOCPhoneNumber = Input.GovPOCPhoneNumber,
                    GovPOCName = Input.GovPOCName,
                    GovPOCEmail = Input.GovPOCEmail,
                    EDI = Input.EDI,
                    IsEnabled = false,
                    IsLockedOut = false,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, AuthorizationConstants.DEFAULT_PASSWORD);
                if (result.Succeeded)
                {
                    _logger.LogInformation(String.Format("User created a new account{0} with password.", Input.Name));
                    //now set up a default role
                    user = await _userManager.FindByNameAsync(user.UserName);
                   
                    var roleResult = _userManager.AddToRoleAsync(user, _configuration["AppSettings:DefaultRole"].ToString());
                    _logger.LogInformation(String.Format("User {0} added to role {1}.", Input.Name, roleResult));
                    return RedirectToPage("./AccountDisabled");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
