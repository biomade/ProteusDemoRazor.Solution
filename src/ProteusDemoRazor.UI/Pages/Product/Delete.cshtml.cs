using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages.Product
{
    [Breadcrumb("Delete", FromPage = typeof(IndexModel))]
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IProductService productService, ILogger<DeleteModel> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Product Delete page");
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            await _productService.Delete(Product);
            return RedirectToPage("./Index");
        }
    }
}