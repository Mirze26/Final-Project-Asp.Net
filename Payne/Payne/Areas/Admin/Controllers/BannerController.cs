using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Payne.Areas.Admin.Helpers;
using Payne.Areas.Admin.ViewModels;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBannerService _bannerService;
        private readonly IWebHostEnvironment _env;


        public BannerController(
                                IBannerService bannerService,
                                IWebHostEnvironment env,
                                AppDbContext context
                                 )
        {
            _bannerService = bannerService;
            _env = env;
            _context = context;

        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Banner> Banner = await _bannerService.GetAllAsync();

            return View(Banner);
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Banner banner = await _bannerService.GetByIdAsync((int)id);

            if (banner is null) return NotFound();

            return View(banner);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerCreateVM banner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Banner newBanner = new Banner();

                foreach (var photo in banner.Photos)
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


                foreach (var photo in banner.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", fileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    newBanner.Image = fileName;
                }




                newBanner.Title = banner.Title;
                newBanner.Name = banner.Name;





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
                Banner banner = await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);

                if (banner is null) return NotFound();

                //string path = Path.Combine(_env.WebRootPath, "assets/image/home", banner.Image);
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/home", banner.Image);
                FileHelper.DeleteFile(path);

                _context.Banners.Remove(banner);

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

            Banner dbBrand = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

            if (dbBrand is null) return NotFound();


            BannerEditVM model = new()
            {
                Id = dbBrand.Id,
                Image = dbBrand.Image,
                Title = dbBrand.Title,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BannerEditVM banner)
        {

            try
            {
                if (id == null) return BadRequest();


                Banner dbBanner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

                if (dbBanner is null) return NotFound();


                BannerEditVM model = new()
                {
                    Id = dbBanner.Id,
                    Image = dbBanner.Image,
                    Title = dbBanner.Title

                };



                if (banner.Photo != null)
                {

                    if (!banner.Photo.ContentType.Contains("image/"))  
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbBanner);
                    }

                    string oldPath = Path.Combine(_env.WebRootPath, "assets/image/home", dbBanner.Image); 


                
                    

                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + banner.Photo.FileName);

                    string newPath = Path.Combine(_env.WebRootPath, "assets/image/home", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create)) 
                    {
                        await banner.Photo.CopyToAsync(stream);
                    }

                    dbBanner.Image = fileName;

                }
                else
                {
                    Banner newBanner = new()
                    {
                        Image = dbBanner.Image
                    };

                    dbBanner.Title = banner.Title;
                }

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
    

