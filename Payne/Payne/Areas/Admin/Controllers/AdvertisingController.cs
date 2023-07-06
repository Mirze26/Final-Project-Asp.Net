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
    public class AdvertisingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdvertisingService _advertisingService;
        private readonly IWebHostEnvironment _env;

        public AdvertisingController(IAdvertisingService advertisingService,
                                     IWebHostEnvironment env,
                                      AppDbContext context)

        {
            _advertisingService = advertisingService;
            _env = env;
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();

            return View(advertisings);
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Advertising advertising = await _advertisingService.GetFullDataByIdAsync((int)id);

            if (advertising is null) return NotFound();

            return View(advertising);
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertisingCreateVM advertising)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!advertising.Photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (!advertising.Photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }




                string fileName = Guid.NewGuid().ToString() + "_" + advertising.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", fileName);

                await FileHelper.SaveFileAsync(path, advertising.Photo);





                Advertising newAdvertising = new()
                {
                    Image = fileName,

                    Name = advertising.Name

                };




                await _context.Advertisings.AddAsync(newAdvertising);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
                catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Advertising advertising = await _context.Advertisings.FirstOrDefaultAsync(b => b.Id == id);

                if (advertising is null) return NotFound();

               
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", advertising.Image);
                FileHelper.DeleteFile(path);

                _context.Advertisings.Remove(advertising);

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

            Advertising dbAdvertising = await _context.Advertisings.FirstOrDefaultAsync(m => m.Id == id);

            if (dbAdvertising is null) return NotFound();


            AdvertisingEditVM model = new()
            {
               
                Photo = dbAdvertising.Photo,
                Name = dbAdvertising.Name,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AdvertisingEditVM advertising)
        {

            try
            {
                if (id == null) return BadRequest();


                Advertising dbAdvertising = await _context.Advertisings.FirstOrDefaultAsync(m => m.Id == id);

                if (dbAdvertising is null) return NotFound();


                AdvertisingEditVM model = new()
                {
                   
                    Photo = dbAdvertising.Photo,
                    Name = dbAdvertising.Name

                };



                if (advertising.Photo != null)
                {

                    if (!advertising.Photo.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbAdvertising);
                    }

                    string oldPath = Path.Combine(_env.WebRootPath, "assets/image/home", dbAdvertising.Image);





                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + advertising.Photo.FileName);

                    string newPath = Path.Combine(_env.WebRootPath, "assets/image/home", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create))
                    {
                        await advertising.Photo.CopyToAsync(stream);
                    }

                    dbAdvertising.Image = fileName;

                }
                else
                {
                    Banner newBanner = new()
                    {
                        Image = dbAdvertising.Image
                    };


                }



               
                dbAdvertising.Name = advertising.Name;


                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }



        }


    }
}
