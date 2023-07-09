using Microsoft.AspNetCore.Mvc;
using Payne.Areas.Admin.Helpers;
using Payne.Areas.Admin.ViewModels;
using Payne.Data;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;

namespace Payne.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChoseUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IChoseUsService _choseUsService;
        private readonly IWebHostEnvironment _env;


        public ChoseUsController(AppDbContext context,
                                   IChoseUsService choseUsService,
                                   IWebHostEnvironment env)

        {
            _context = context;
            _env = env;
            _choseUsService = choseUsService;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable<ChoseUs> choseUs = await _choseUsService.GetAllAsync();

            return View(choseUs);
        }






        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            ChoseUs choseUs = await _choseUsService.GetByIdAsync((int)id);

            if (choseUs is null) return NotFound();

            return View(choseUs);
        }







        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutInfoCreateVM aboutInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!aboutInfo.Photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (!aboutInfo.Photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }




                string fileName = Guid.NewGuid().ToString() + "_" + aboutInfo.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/about", fileName);

                await FileHelper.SaveFileAsync(path, aboutInfo.Photo);





                AboutInfo newAbout = new()
                {
                    Image = fileName,

                    Title = aboutInfo.Title,

                    Description = aboutInfo.Description,

                };




                await _context.AboutInfos.AddAsync(newAbout);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
