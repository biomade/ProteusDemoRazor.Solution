using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Proteus.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class AccountLockedModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AccountLockedModel(IConfiguration configuration)
        {            
            _configuration = configuration;
        }
        public void OnGet()
        {
            ViewData["LockDays"] = _configuration["AppSettings:MaxDaysBetweenLogins"];
        }
    }
}
