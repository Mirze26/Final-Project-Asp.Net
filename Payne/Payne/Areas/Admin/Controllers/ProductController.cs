using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payne.Areas.Admin.Helpers;
using Payne.Areas.Admin.ViewModels;
using Payne.Data;
using Payne.Helpers;
using Payne.Helpers.Enums;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Payne.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;

        public ProductController(AppDbContext context, IWebHostEnvironment env, IProductService productService, IBrandService brandService, IColorService colorService)
        {
            _context = context;
            _env = env;
            _productService = productService;
            _brandService = brandService;
            _colorService = colorService;

        }



        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            IEnumerable<Product> products = await _productService.GetPaginatedDatas(page, take);

            IEnumerable<ProductListVM> mappedDatas = GetMappedDatas(products);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ViewBag.take = take;

            return View(paginatedDatas);
        }



        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }


        private IEnumerable<ProductListVM> GetMappedDatas(IEnumerable<Product> products)
        {
            List<ProductListVM> mappedDatas = new();

            foreach (var product in products)
            {
                ProductListVM prodcutVM = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    MainImage = product.ProductImages.Where(m => m.Ismain).FirstOrDefault()?.Image
                };

                mappedDatas.Add(prodcutVM);

            }
            return mappedDatas;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {


            ViewBag.color = await GetColorAsync();

            ViewBag.brand = await GetBrandAsync();

            return View();
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {

            try
            {


                ViewBag.color = await GetColorAsync();

                ViewBag.brand = await GetBrandAsync();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                foreach (var photo in model.Photos)
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

                List<ProductImage> productImages = new();

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/shop", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);

                }

                productImages.FirstOrDefault().Ismain = true;

                decimal convertedPrice = decimal.Parse(model.Price);

                Product newProduct = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    BrandId = model.BrandId,
                    ColorId = model.ColorId,
                    Sku = model.Sku,
                    Rating = model.Rating,
                    Count = model.Count,
                    Price = convertedPrice,
                    ProductImages = productImages,

                };

                await _context.ProductImages.AddRangeAsync(productImages);
                await _context.Products.AddAsync(newProduct);
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

            Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return NotFound();


            ViewBag.color = await GetColorAsync();

            ViewBag.brand = await GetBrandAsync();



            return View(new ProductEditVM
            {
                Name = dbProduct.Name,
                Description = dbProduct.Description,
                ColorId = dbProduct.ColorId,
                Images = dbProduct.ProductImages,
                Sku = dbProduct.Sku,
                Rating = dbProduct.Rating,
                Count = dbProduct.Count,
                Price = dbProduct.Price.ToString("0.#####").Replace(",", "."),
                BrandId = dbProduct.BrandId,
                


            });


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM updatedProduct)
        {
            ViewBag.color = await GetColorAsync();

            ViewBag.brand = await GetBrandAsync();

            if (!ModelState.IsValid) return View(updatedProduct);

            Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return NotFound();

            if (updatedProduct.Photos != null)
            {

                foreach (var photo in updatedProduct.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(dbProduct);
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(dbProduct);
                    }



                }

                foreach (var item in dbProduct.ProductImages)
                {

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/shop", item.Image);

                    FileHelper.DeleteFile(path);
                }



                List<ProductImage> productImages = new();

                foreach (var photo in updatedProduct.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;


                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/shop", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);

                }

                productImages.FirstOrDefault().Ismain = true;

                dbProduct.ProductImages = productImages;
            }

            decimal convertedPrice = decimal.Parse(updatedProduct.Price);

            dbProduct.Name = updatedProduct.Name;
            dbProduct.Description = updatedProduct.Description;
            dbProduct.Price = convertedPrice;
            dbProduct.Count= updatedProduct.Count;
            dbProduct.Sku= updatedProduct.Sku;
            dbProduct.Rating= updatedProduct.Rating;
            dbProduct.BrandId = updatedProduct.BrandId;
            dbProduct.ColorId = updatedProduct.ColorId;


            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

            ViewBag.color = await GetColorAsync();

            ViewBag.brand = await GetBrandAsync();

            return View(new ProductDetailVM
            {

                Name = dbProduct.Name,
                Description = dbProduct.Description,
                BrandName = dbProduct.Brand.Name,
                Images = dbProduct.ProductImages,
                Sku = dbProduct.Sku,
                Rating = dbProduct.Rating,
                Count = dbProduct.Count,
                Price = dbProduct.Price.ToString("0.#####").Replace(",", "."),
                ColorName = dbProduct.Color.Name,

            });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();


            foreach (var item in product.ProductImages)
            {

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/image/shop", item.Image);

                FileHelper.DeleteFile(path);

            }


            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        private async Task<SelectList> GetColorAsync()
        {

            IEnumerable<Color> categories = await _colorService.GetAllAsync();

            return new SelectList(categories, "Id", "Name");


        }

        private async Task<SelectList> GetBrandAsync()
        {

            IEnumerable<Brand> categories = await _brandService.GetAllAsync();

            return new SelectList(categories, "Id", "Name");


        }


    }
}
