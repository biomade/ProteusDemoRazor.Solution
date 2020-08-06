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

        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public DODViewModel Input { get; set; }
        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, IApplicationConfiguration configuration)
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

            if (Input.DODAccept == false)
            {
                ModelState.AddModelError(string.Empty, "You MUST accept the DoD statement before you can use this website.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                return RedirectToPage("./login2");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}

