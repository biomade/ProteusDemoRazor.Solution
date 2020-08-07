using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account.Users;
using Proteus.Core.Constants;
using Proteus.Core.Entities.Identity;
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
        public UserEditViewModel Input { get; set; }

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
            Input = new UserEditViewModel();
            Input.Id = user.Id;
            Input.Email = user.Email;
            Input.FirstName = user.FirstName;
            Input.GovPOCEmail = user.GovPOCEmail;
            Input.GovPOCName = user.GovPOCName;
            Input.GovPOCPhoneNumber = user.GovPOCPhoneNumber;
            Input.IsEnabled = user.IsEnabled;
            Input.IsLockedOut = user.IsLockedOut;
            Input.LastName = user.LastName;
            Input.MI = user.MI;
            Input.Phone = user.PhoneNumber;
            Input.UserName = user.UserName;
            Input.UserOnLine = user.UserOnLine;
            Input.EDI = user.EDI;
            Input.LastLoginDate = user.LastLoginDate;
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
                var user = await _userManager.FindByIdAsync(Input.Id.ToString());
                user.Email = Input.Email;
                user.FirstName = Input.FirstName;
                user.GovPOCEmail = Input.GovPOCEmail;
                user.GovPOCName = Input.GovPOCName;
                user.GovPOCPhoneNumber = Input.GovPOCPhoneNumber;
                user.IsEnabled = Input.IsEnabled;
                user.IsLockedOut = Input.IsLockedOut;
                user.LastName = Input.LastName;
                user.MI = Input.MI;                
                user.PhoneNumber = Input.Phone;
                user.UserName = Input.UserName;
                user.UserOnLine = Input.UserOnLine;
                user.ModifiedDate = System.DateTime.Now;
                user.NormalizedEmail = Input.Email.ToUpper();
                user.EDI = Input.EDI;
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Input.Id))
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
