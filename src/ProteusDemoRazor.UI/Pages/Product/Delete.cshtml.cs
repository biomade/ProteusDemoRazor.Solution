using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.Mapper;
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
        public ProductViewModel ProductVM { get; set; }

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

            ProductVM = ObjectMapper.Mapper.Map<ProductViewModel>(product);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product =  ObjectMapper.Mapper.Map<Proteus.Core.Entities.Product>(ProductVM);
            await _productService.Delete(product);
            return RedirectToPage("./Index");
        }
    }
}