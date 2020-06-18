﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proteus.UI.Interfaces;
using Proteus.UI.ViewModels;

namespace Proteus.UI.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly IProductPageService _productPageService;

        public DeleteModel(IProductPageService productPageService)
        {
            _productPageService = productPageService ?? throw new ArgumentNullException(nameof(productPageService));
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _productPageService.GetProductById(productId.Value);
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

            await _productPageService.DeleteProduct(Product);
            return RedirectToPage("./Index");
        }
    }
}