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
    [Breadcrumb("Roles")]
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(RoleManager<Role> roleManager, ILogger<IndexModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public IList<Role> Inputs { get;set; }

        public async Task OnGetAsync()
        {
            Inputs =  _roleManager.Roles.AsQueryable().ToList();
        }
    }
}
