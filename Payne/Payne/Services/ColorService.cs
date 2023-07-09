using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;

        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Color>> GetAllAsync() => await _context.Colors.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<Color> GetByIdAsync(int id) => await _context.Colors.FindAsync(id);
    }
}
