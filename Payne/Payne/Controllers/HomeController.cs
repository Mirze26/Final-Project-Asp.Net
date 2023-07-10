using Microsoft.AspNetCore.Mvc;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;
using Payne.ViewModels;

namespace Payne.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IAdvertisingService _advertisingService;
        private readonly IProductService _productService;


        public HomeController(AppDbContext context,
                      ISliderService sliderService,
                      IBannerService bannerService,
                      IAdvertisingService advertisingService,
                       IProductService productService)


        {
            _context = context;
            _sliderService = sliderService;
            _bannerService = bannerService;
            _advertisingService = advertisingService;
            _productService = productService;
        }



        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();
            IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();



            HomeVM model = new()
            {
                Sliders = sliders,
                Banners = banners,
                Advertisings = advertisings,
                Products = products

            };

            return View(model);
        }
    }
}
