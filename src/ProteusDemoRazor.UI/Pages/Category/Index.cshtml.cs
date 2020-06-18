using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;

namespace Proteus.UI.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IEnumerable<CategoryViewModel> CategoryList { get; set; } = new List<CategoryViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            CategoryList = (IEnumerable<CategoryViewModel>)await _categoryService.GetCategoryList();
            return Page();
        }
    }
}