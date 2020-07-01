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
    [Breadcrumb("Product")]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(IProductService productService, ILogger<IndexModel> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Product Index page");
        }

        public IEnumerable<ProductViewModel> ProductList { get; set; } = new List<ProductViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productService.GetProducts(SearchTerm);
            return Page();
        }
    }
}