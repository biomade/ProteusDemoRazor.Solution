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
    [Breadcrumb("Delete", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(UserManager<User> userManager, ILogger<DeleteModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public User Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Input = await _userManager.FindByIdAsync(id.ToString());

            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Input = await _userManager.FindByIdAsync(id.ToString());

            if (Input != null)
            {
                //this will delete the user roles too
             var result =   await  _userManager.DeleteAsync(Input); 
                
            }

            return RedirectToPage("./Index");
        }
    }
}
