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
    [Breadcrumb("Users")]
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly Proteus.Infrastructure.Identity.IdentityDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(UserManager<User> userManager, ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IList<User> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users =  _userManager.Users.AsQueryable().ToList(); 
        }
    }
}
