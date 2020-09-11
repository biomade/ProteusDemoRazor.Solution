using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages.Product
{
    [Breadcrumb("Details", FromPage = typeof(IndexModel))]
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IProductService productService, ILogger<DetailsModel> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Product Details page");
        }

        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(productId.Value);
            if (product == null)
            {
                return NotFound();
            }
            Product = Application.Mapper.ObjectMapper.Mapper.Map<ProductViewModel>(product);
            return Page();
        }
    }
}