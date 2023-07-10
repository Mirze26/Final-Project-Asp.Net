using Microsoft.AspNetCore.Mvc;
using Payne.Data;
using Payne.Helpers;
using Payne.Models;
using Payne.Services;
using Payne.Services.Interfaces;
using Payne.ViewModels.BlogVM;
using Payne.ViewModels.Shop;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Payne.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;

        public BlogController(AppDbContext context,
                              IBlogService blogService)

        {
            _blogService = blogService;
            _context = context;
        }




        public async Task <IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();
            BlogVM model = new()
            {
                Blogs = blogs,

            };

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            return View(id);
        }
    }
}
