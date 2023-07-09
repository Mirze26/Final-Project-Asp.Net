using Microsoft.AspNetCore.Mvc;
using Payne.Data;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;
using Payne.ViewModels;
using Payne.ViewModels.Shop;

namespace Payne.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;

        public ShopController(AppDbContext context,
                              IColorService colorService,
                              IBrandService brandService)

        {
            _colorService = colorService;
            _context = context;
            _brandService = brandService;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();


            ShopVM model = new()
            {
                Colors = colors,
                Brands = brands
           
            };


            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            return View(id);
        }
    }
}
