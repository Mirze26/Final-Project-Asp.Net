using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync() => await _context.Brands.Where(s => !s.SoftDelete).ToListAsync();

        public async Task<Brand> GetByIdAsync(int id) => await _context.Brands.FindAsync(id);
    }
}
