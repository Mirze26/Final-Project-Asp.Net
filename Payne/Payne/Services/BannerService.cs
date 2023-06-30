using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banner>> GetAllAsync() => await _context.Banners.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<Banner> GetByIdAsync(int id) => await _context.Banners.FindAsync(id);

    }
}
