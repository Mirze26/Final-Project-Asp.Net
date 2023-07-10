using Microsoft.AspNetCore.Mvc;
using Payne.Data;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;
using Payne.ViewModels;
using Payne.ViewModels.Shop;
using System.Reflection.Metadata;

namespace Payne.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;


        public ShopController(AppDbContext context,
                              IColorService colorService,
                              IBrandService brandService,
                              IProductService productService)

        {
            _colorService = colorService;
            _context = context;
            _brandService = brandService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();


            ShopVM model = new()
            {
                Colors = colors,
                Brands = brands,
                Products = products 
           
            };


            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Product product = await _productService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();

            IEnumerable<Product> productAll = await _productService.GetAllAsync();

            return View(new ProductDetailVM
            {

                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ColorName = product.Color.Name,
                Images = product.ProductImages,
                Products = productAll,
                BrandName = product.Brand.Name,
                Sku= product.Sku,
                Price =product.Price,
                Rating = product.Rating,
                Count = product.Count,
              


            });

        }
    }
}
