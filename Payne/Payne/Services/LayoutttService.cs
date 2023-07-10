using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.ViewModels;

namespace Payne.Services
{
    public class LayoutttService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutttService(AppDbContext context, IHttpContextAccessor accessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }
        public BasketVm GetBaskets()
        {
            BasketVm basketData = new BasketVm()
            {
                TotalPrice = 0,
                BasketItems = new(),
                Count = 0
            };
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {

                List<BasketItem> basketItems = _context.BasketItems.Include(x=>x.Product).Include(b => b.AppUser).Where(b => b.AppUser.UserName == _accessor.HttpContext.User.Identity.Name).ToList();
                foreach (BasketItem item in basketItems)
                {
                    Product product = _context.Products.FirstOrDefault(f => f.Id == item.ProductId);
                    if (product != null)
                    {
                        BasketItem basket = new BasketItem()
                        {
                            Product = product,
                            Count = item.Count
                        };
                        basket.Product.Price = product.Price;
                        basketData.BasketItems.Add(basket);
                        basketData.Count++;
                        basketData.TotalPrice += item.Product.Price * item.Count;
                    }
                }
            }

            return basketData;
        }


        public List<Product> GetProducts()
        {
            List<Product> products=_context.Products.Include(x=>x.ProductImages).ToList();
            return products;    
        }
     

    }
}
