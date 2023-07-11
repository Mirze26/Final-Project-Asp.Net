using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payne.Areas.Admin.Helpers;
using Payne.Areas.Admin.ViewModels;
using Payne.Data;
using Payne.Helpers.Enums;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;

namespace Payne.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class AboutInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutInfoService _aboutInfoService;
        private readonly IWebHostEnvironment _env;


        public AboutInfoController(AppDbContext context,
                                   IAboutInfoService aboutInfoService,
                                   IWebHostEnvironment env)

        {
            _context = context;
            _env = env;
            _aboutInfoService = aboutInfoService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<AboutInfo> aboutInfo = await _aboutInfoService.GetAllAsync();

            return View(aboutInfo);
        }





        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            AboutInfo aboutInfo = await _aboutInfoService.GetByIdAsync((int)id);

            if (aboutInfo is null) return NotFound();

            return View(aboutInfo);
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





        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                AboutInfo aboutInfo = await _context.AboutInfos.FirstOrDefaultAsync(b => b.Id == id);

                if (aboutInfo is null) return NotFound();


                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/about", aboutInfo.Image);
                FileHelper.DeleteFile(path);

                _context.AboutInfos.Remove(aboutInfo);

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

            AboutInfo dbAboutInfo = await _context.AboutInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (dbAboutInfo is null) return NotFound();


            AboutInfoEditVM model = new()
            {
                Description = dbAboutInfo.Description,
                Photo = dbAboutInfo.Photo,
                Title = dbAboutInfo.Title,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutInfoEditVM aboutInfo)
        {

            try
            {
                if (id == null) return BadRequest();


                AboutInfo dbAboutInfo = await _context.AboutInfos.FirstOrDefaultAsync(m => m.Id == id);

                if (dbAboutInfo is null) return NotFound();


                AboutInfoEditVM model = new()
                {
                    Description = dbAboutInfo.Description,
                    Photo = dbAboutInfo.Photo,
                    Title = dbAboutInfo.Title

                };



                if (aboutInfo.Photo != null)
                {

                    if (!aboutInfo.Photo.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbAboutInfo);
                    }

                    string oldPath = Path.Combine(_env.WebRootPath, "assets/image/about", dbAboutInfo.Image);





                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + aboutInfo.Photo.FileName);

                    string newPath = Path.Combine(_env.WebRootPath, "assets/image/about", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create))
                    {
                        await aboutInfo.Photo.CopyToAsync(stream);
                    }

                    dbAboutInfo.Image = fileName;

                }
                else
                {
                    AboutInfo newAboutInfo = new()
                    {
                        Image = dbAboutInfo.Image
                    };


                }



                dbAboutInfo.Title = aboutInfo.Title;
                dbAboutInfo.Description = aboutInfo.Description;


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
