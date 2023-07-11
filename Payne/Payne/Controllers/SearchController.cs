using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
