using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payne.Areas.Admin.Helpers;
using Payne.Areas.Admin.ViewModels;
using Payne.Data;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;

namespace Payne.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,
                                ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

            return View(sliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Slider newSlider = new Slider();

                foreach (var photo in slider.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {

                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();

                    }

                    if (!photo.CheckFileSize(200))
                    {

                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();

                    }
                }


                foreach (var photo in slider.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", fileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    newSlider.Image = fileName;
                }


                if (!slider.BackGroundPhoto.CheckFileType("image/"))
                {

                    ModelState.AddModelError("BackGroundPhoto", "File type must be image");
                    return View();

                }

                if (!slider.BackGroundPhoto.CheckFileSize(200))
                {

                    ModelState.AddModelError("BackGroundPhoto", "Image size must be max 200kb");
                    return View();

                }

                string fileNameBackgroundImage = Guid.NewGuid().ToString() + "_" + slider.BackGroundPhoto.FileName;

                string BackgroundImagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", fileNameBackgroundImage);

                using (FileStream stream = new FileStream(BackgroundImagePath, FileMode.Create))
                {
                    await slider.BackGroundPhoto.CopyToAsync(stream);
                }

                newSlider.BackgroundImage = fileNameBackgroundImage;


                newSlider.Title = slider.Title;
                newSlider.Description = slider.Description;
                newSlider.Offer = slider.Offer;


                await _context.Sliders.AddAsync(newSlider);

                

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

                if (slider is null) return NotFound();



                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", slider.Image);

                FileHelper.DeleteFile(path);

                _context.Sliders.Remove(slider);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM slider)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
                if (dbSlider == null) return NotFound();

                SliderEditVM model = new()
                {
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    Description = dbSlider.Description,
                    Offer = dbSlider.Offer,
                };

                if (!ModelState.IsValid) return View(model);


                if (slider.Photo is not null)
                {
                    if (!slider.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!slider.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(model);
                    }
                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", dbSlider.Image);
                    FileHelper.DeleteFile(oldPath);

                    dbSlider.Image = slider.Photo.CreateFile(_env, "assets/image/home");
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }

                dbSlider.Title = slider.Title;
                dbSlider.Description = slider.Description;
                dbSlider.Offer = slider.Offer;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




    }
}
