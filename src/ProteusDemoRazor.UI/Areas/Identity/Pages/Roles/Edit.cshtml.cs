﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Areas.Identity.Pages.Roles
{
    [Breadcrumb("Edit", FromPage = typeof(IndexModel))]
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<EditModel> _logger;

        public EditModel(RoleManager<Role> roleManager, ILogger<EditModel> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
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
                //Possibly don't allow role name to change 
                //in case it is used for authorization which will mess things up
                Role.NormalizedName = Role.Name.ToUpper();
                Role.ModifiedDate = System.DateTime.Now;
                await _roleManager.UpdateAsync(Role);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Role.Id))
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

        private bool RoleExists(int id)
        {
            var role= _roleManager.FindByIdAsync(id.ToString()).Result;
            
            return (role != null) ? true: false;
        }
    }
}
