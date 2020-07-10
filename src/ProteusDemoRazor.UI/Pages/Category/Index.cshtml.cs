using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;
using Proteus.Core.Entities.Identity;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages.Category
{
    [Breadcrumb("Category")]
    //[Authorize(Roles="Visitor")]
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ICategoryService categoryService, ILogger<IndexModel> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into CategoryIndex page");
        }

        public IEnumerable<CategoryViewModel> CategoryList { get; set; } = new List<CategoryViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            CategoryList = (IEnumerable<CategoryViewModel>)await _categoryService.GetCategoryList();
            return Page();
        }
    }
}