using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;

namespace Proteus.UI.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IProductService productService, ILogger<CreateModel> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Product Create page");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _productService.GetCategoryList();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return Page();
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product = await _productService.Create(Product);
            return RedirectToPage("./Index");
        }
    }
}