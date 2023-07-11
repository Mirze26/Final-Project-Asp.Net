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
        public async Task<IActionResult> Create(ChoseUsCreateVM choseUs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!choseUs.Photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (!choseUs.Photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }




                string fileName = Guid.NewGuid().ToString() + "_" + choseUs.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/about", fileName);

                await FileHelper.SaveFileAsync(path, choseUs.Photo);





                ChoseUs newchoseUs = new()
                {
                    Image = fileName,

                    Name = choseUs.Name,

                    Description = choseUs.Description,

                };




                await _context.Choses.AddAsync(newchoseUs);
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
                ChoseUs choseUs = await _context.Choses.FirstOrDefaultAsync(b => b.Id == id);

                if (choseUs is null) return NotFound();


                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/about", choseUs.Image);
                FileHelper.DeleteFile(path);

                _context.Choses.Remove(choseUs);

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

            ChoseUs dbChoseUs = await _context.Choses.FirstOrDefaultAsync(m => m.Id == id);

            if (dbChoseUs is null) return NotFound();


            ChoseUsEditVM model = new()
            {
                Description = dbChoseUs.Description,
                Photo = dbChoseUs.Photo,
                Name = dbChoseUs.Name,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ChoseUsEditVM chose)
        {

            try
            {
                if (id == null) return BadRequest();


                ChoseUs dbchoseUs = await _context.Choses.FirstOrDefaultAsync(m => m.Id == id);

                if (dbchoseUs is null) return NotFound();


                ChoseUsEditVM model = new()
                {
                    Description = chose.Description,
                    Photo = chose.Photo,
                    Name = chose.Name

                };



                if (chose.Photo != null)
                {

                    if (!chose.Photo.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbchoseUs);
                    }

                    string oldPath = Path.Combine(_env.WebRootPath, "assets/image/about", dbchoseUs.Image);





                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + chose.Photo.FileName);

                    string newPath = Path.Combine(_env.WebRootPath, "assets/image/about", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create))
                    {
                        await chose.Photo.CopyToAsync(stream);
                    }

                    dbchoseUs.Image = fileName;

                }
                else
                {
                    ChoseUs newchoseUs = new()
                    {
                        Image = dbchoseUs.Image
                    };


                }



                dbchoseUs.Name = chose.Name;
                dbchoseUs.Description = chose.Description;


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
