using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;

        public BasketController(AppDbContext context,UserManager<AppUser> userManager, IProductService productService)
        {
            _context = context;
            _userManager = userManager;
            _productService = productService;
        }
        public async Task<IActionResult> AddBasket(int id, int quantity)
        {

            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem basketItem = _context.BasketItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (basketItem == null)
                {
                    basketItem = new BasketItem()
                    {
                        AppUserId = user.Id,
                        ProductId = product.Id,
                        Count = quantity
                    };
                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    basketItem.Count++;
                }
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> RemoveBasketItem(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem basketItem = _context.BasketItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (basketItem != null)
                {

                    _context.BasketItems.Remove(basketItem);
                    _context.SaveChanges();
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }




 

        public async Task<IActionResult> BasketProductCountChange(int basketId, int quantity)
        {
            if (basketId < 1) return NotFound();
            BasketItem item = _context.BasketItems.FirstOrDefault(x => x.Id == basketId);
            if (item is null) return NotFound();

            item.Count = item.Count + quantity;

            if (item.Count == 0) return RedirectToAction("Index", "Cart");

            Product product = await _productService.GetFullDataByIdAsync(item.ProductId);

         
            _context.SaveChanges();
            //item.Count= ++quantity;
            return RedirectToAction("Index", "Cart");
        }
    }
}
