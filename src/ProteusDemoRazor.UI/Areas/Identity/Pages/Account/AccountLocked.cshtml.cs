using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Proteus.Application.Interfaces;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class AccountLockedModel : PageModel
    {
        private readonly IApplicationConfiguration _configuration;

        public AccountLockedModel(IApplicationConfiguration configuration)
        {            
            _configuration = configuration;
        }
        public void OnGet()
        {
            ViewData["LockDays"] = _configuration.MaxDaysBetweenLogins;
        }
    }
}
