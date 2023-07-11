using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;

namespace Payne.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WishListController(AppDbContext context,
                              UserManager<AppUser> userManager)

        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = new();

            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user is null) return NotFound();
                List<WishListItem> wishListItems = _context.WishItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == user.Id).ToList();
                return View(wishListItems);
            }

            else
            {
                List<BasketItem> basketItems = new();
                return View(basketItems);
            }



        }
        public async Task<IActionResult> AddWishList(int id)
        {

            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                WishListItem wishListItem = _context.WishItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (wishListItem == null)
                {
                    wishListItem = new WishListItem()
                    {
                        AppUserId = user.Id,
                        ProductId = product.Id,
    
                    };
                    _context.WishItems.Add(wishListItem);
                }
            
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }



        public async Task<IActionResult> RemoveWishlistItem(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                WishListItem basketItem = _context.WishItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (basketItem != null)
                {

                    _context.WishItems.Remove(basketItem);
                    _context.SaveChanges();
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
