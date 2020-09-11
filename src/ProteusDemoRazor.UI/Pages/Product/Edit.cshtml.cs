using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.Mapper;
using Proteus.Application.ViewModels;
using SmartBreadcrumbs.Attributes;
using Proteus.Core.Entities;

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
        public ProductViewModel ProductVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(productId.Value);
            ProductVM = ObjectMapper.Mapper.Map<ProductViewModel>(product);
            if (ProductVM == null)
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
                //map the vm to the model
                var product = ObjectMapper.Mapper.Map<Proteus.Core.Entities.Product>(ProductVM);
                await _productService.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(ProductVM.Id))
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