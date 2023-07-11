using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;
using System.Reflection.Metadata;

namespace Payne.Services
{
    public class ProductService :IProductService
    {

        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(m=>m.ProductImages).Include(m=>m.Color).Include(m=>m.Brand).ToListAsync(); 

        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetFullDataByIdAsync(int id) => await _context.Products.Include(m => m.ProductImages).Include(m => m.Color).Include(m => m.Brand)?.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<Product>> SearchAsync(string searchText) => await _context.Products.Include(ci => ci.ProductImages).Where(c => c.Name.Trim().ToLower().Contains(searchText.Trim().ToLower())).ToListAsync();




        public async Task<IEnumerable<Product>> GetPaginatedDatas(int page, int take)
        {
            return await _context.Products.Include(m => m.ProductImages).Include(m => m.Color).Include(m => m.Brand)?.Skip((page * take) - take).Take(take).ToListAsync();


        }



        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

    }
}
