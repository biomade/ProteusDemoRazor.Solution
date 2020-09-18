using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Proteus.Application.Interfaces;
using Proteus.Application.Mapper;
using Proteus.Application.ViewModels;
using SmartBreadcrumbs.Attributes;

namespace Proteus.UI.Pages.Product
{
    [Breadcrumb("Create", FromPage = typeof(IndexModel))]
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
            CategoryList = categories.Select(c =>
                                  new SelectListItem
                                  {
                                      Value = c.Id.ToString(),
                                      Text = c.CategoryName
                                  }).ToList();

            //ViewData["CategoryId"] = selectListItems;
            return Page();
        }

        [BindProperty]
        public List<SelectListItem> CategoryList { get; set; }
        
        [BindProperty]
        public ProductViewModel ProductVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mappedEntity = ObjectMapper.Mapper.Map<Proteus.Core.Entities.Product>(ProductVM);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");
            var product = await _productService.Create(mappedEntity);
            ProductVM = ObjectMapper.Mapper.Map<ProductViewModel>(product);
            return RedirectToPage("./Index");
        }
    }
}