
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Users
{
    [Breadcrumb("Users")]
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        //private readonly Proteus.Infrastructure.Identity.IdentityDbContext _context; //for the unit tests
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(UserManager<User> userManager, ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IList<User> Inputs { get;set; }

        public void OnGetAsync()
        {
            Inputs =  _userManager.Users.AsQueryable().ToList(); 
        }
    }
}
