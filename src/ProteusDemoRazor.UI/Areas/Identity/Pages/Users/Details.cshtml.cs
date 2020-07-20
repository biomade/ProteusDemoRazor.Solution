using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    [Breadcrumb("Details", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(UserManager<User> userManager, ILogger<DetailsModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _userManager.FindByIdAsync(id.ToString());

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
