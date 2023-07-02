using Microsoft.AspNetCore.Mvc;
using Payne.Models;
using Payne.Services.Interfaces;
using Payne.ViewModels.About;

namespace Payne.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutInfoService _aboutInfoService;
        private readonly IChoseUsService _choseUsService;
        private readonly IAboutGalleryService _aboutGalleryService;

        public AboutController(IAboutInfoService aboutInfoService,
                               IChoseUsService choseUsService,
                               IAboutGalleryService aboutGalleryService)
        {
            _aboutInfoService = aboutInfoService;
            _choseUsService = choseUsService;
            _aboutGalleryService = aboutGalleryService;

        }

        public async Task<IActionResult> Index()
        {
            AboutVM model = new()
            {
                AboutInfos = await _aboutInfoService.GetAllAsync(),
                Choses = await _choseUsService.GetAllAsync(),
                AboutGalleries = await _aboutGalleryService.GetAllAsync(),


            };

            return View(model);
        }
    }
}
