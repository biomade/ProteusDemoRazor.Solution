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

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    [Breadcrumb("Details", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateModel> _logger;

        public DetailsModel(RoleManager<Role> roleManager, ILogger<CreateModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _roleManager.FindByIdAsync(id.ToString());

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
