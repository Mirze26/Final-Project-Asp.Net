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

        public HomeController(AppDbContext context,
                      ISliderService sliderService,
                      IBannerService bannerService)


        {
            _context = context;
            _sliderService = sliderService;
            _bannerService = bannerService;

        }



        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();


            HomeVM model = new()
            {
                Sliders = sliders,
                Banners = banners
            };

            return View(model);
        }
    }
}
