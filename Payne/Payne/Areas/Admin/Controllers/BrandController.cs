using Microsoft.AspNetCore.Mvc;
using Payne.Areas.Admin.ViewModels;
using Payne.Areas.Admin.Views.Color;
using Payne.Data;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;
using System.Drawing;

namespace Payne.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IBrandService _brandService;

        public BrandController(AppDbContext context,
                               IBrandService brandService,
                               IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
           _brandService = brandService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands1 = await _brandService.GetAllAsync();

            List<Brand> brands = new();

            foreach (var brand in brands)
            {
                Brand mappedData = new()
                {
                   Id = brand.Id,
                   Name = brand.Name,
                };


            }
            return View(brands1);

        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Brand brand = new()
            {
                Name = model.Name
             
            };

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Brand brand = await _brandService.GetByIdAsync(id);

            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Brand brand = await _brandService.GetByIdAsync(id);

            BrandEditVM model = new()
            {
                Name = brand.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BrandEditVM model)
        {
            if (id == null) return BadRequest();

            Brand brand = await _brandService.GetByIdAsync((int)id);

            if (brand == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            brand.Name = model.Name;


            _context.Brands.Update(brand);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
