using Microsoft.AspNetCore.Mvc;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _productService;

        public SearchController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> SearchByProducts(string searchText)
        {
            IEnumerable<Product> products = await _productService.SearchAsync(searchText);

            return View(products);
        }
    }
}
