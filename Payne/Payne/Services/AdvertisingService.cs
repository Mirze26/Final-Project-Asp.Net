using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly AppDbContext _context;

        public AdvertisingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Advertising>> GetAllAsync() => await _context.Advertisings.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Advertising> GetFullDataByIdAsync(int id) => await _context.Advertisings.FindAsync(id);

    }
}
