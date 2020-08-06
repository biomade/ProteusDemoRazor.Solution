using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Core.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Proteus.Application.Interfaces;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IApplicationConfiguration _configuration;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger, IApplicationConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public RegisterViewModel Input { get; set; }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
           
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Name,
                    Email = Input.Email,
                    PhoneNumber = Input.Phone,
                    GovPOCPhoneNumber = Input.GovPOCPhoneNumber,
                    GovPOCName = Input.GovPOCName,
                    GovPOCEmail = Input.GovPOCEmail,
                    EDI = "1234567890", // Input.EDI,
                    IsEnabled = false,
                    IsLockedOut = false,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //now set up a default role
                    user = await _userManager.FindByNameAsync(user.UserName);
                   
                    var roleResult = _userManager.AddToRoleAsync(user, _configuration.DefaultRole);
                    _logger.LogInformation("User created a new account with password.");
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
