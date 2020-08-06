using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //reset online flag
            var user = await _signInManager.UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            user.UserOnLine = false;
            await _signInManager.UserManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Page();
        }
    }
}
