using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
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
        public User Input { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Input.NormalizedUserName = Input.UserName.ToUpper();
            Input.NormalizedEmail = Input.Email.ToUpper();
            Input.PasswordHash = _passwordHasher.HashPassword(Input, Input.PasswordHash);

            if (Input.IsEnabled)
            {
                Input.LastLoginDate = System.DateTime.Now;
            }
            Input.CreatedDate = System.DateTime.Now;
            //hash the password entered


            IdentityResult result =  await _userManager.CreateAsync(Input);
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
            Input = await _userManager.FindByNameAsync(Input.UserName);

            var roleResult = _userManager.AddToRoleAsync(Input, _configuration.DefaultRole);

            return RedirectToPage("./Index");
        }
    }
}
