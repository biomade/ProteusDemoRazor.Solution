using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    [Breadcrumb("Edit", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public EditModel(UserManager<User> userManager, ILogger<EditModel> logger, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public UserEditViewModel Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }
            Item = new UserEditViewModel();
            Item.Id = user.Id;
            Item.Email = user.Email;
            Item.FirstName = user.FirstName;
            Item.GovPOCEmail = user.GovPOCEmail;
            Item.GovPOCName = user.GovPOCName;
            Item.GovPOCPhoneNumber = user.GovPOCPhoneNumber;
            Item.IsEnabled = user.IsEnabled;
            Item.IsLockedOut = user.IsLockedOut;
            Item.LastName = user.LastName;
            Item.MI = user.MI;
            Item.Phone = user.PhoneNumber;
            Item.UserName = user.UserName;
            Item.UserOnLine = user.UserOnLine;


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = await _userManager.FindByIdAsync(Item.Id.ToString());
                user.Email = Item.Email;
                user.FirstName = Item.FirstName;
                user.GovPOCEmail = Item.GovPOCEmail;
                user.GovPOCName = Item.GovPOCName;
                user.GovPOCPhoneNumber = Item.GovPOCPhoneNumber;
                user.IsEnabled = Item.IsEnabled;
                user.IsLockedOut = Item.IsLockedOut;
                user.LastName = Item.LastName;
                user.MI = Item.MI;
                if (!string.IsNullOrEmpty(Item.Password))
                {
                    //if not empty update the password
                    user.PasswordHash = _passwordHasher.HashPassword(user, Item.Password);
                }
                user.PhoneNumber = Item.Phone;
                user.UserName = Item.UserName;
                user.UserOnLine = Item.UserOnLine;
                user.ModifiedDate = System.DateTime.Now;
                user.NormalizedEmail = Item.Email.ToUpper();
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Item.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;

            return (user != null) ? true : false;
        }
    }
}
