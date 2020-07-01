using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages.Product
{
    [Breadcrumb("Edit", FromPage = typeof(IndexModel))]
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IProductService productService, ILogger<EditModel> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Product Edit page");
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _productService.GetProductById(productId.Value);
            if (Product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(await _productService.GetCategoryList(), "Id", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _productService.Update(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
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

        private bool ProductExists(int id)
        {
            var product = _productService.GetProductById(id);
            return product != null;
        }
    }
}