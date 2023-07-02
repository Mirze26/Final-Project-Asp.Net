using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class ChoseUsService : IChoseUsService
    {
        private readonly AppDbContext _context;

        public ChoseUsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChoseUs>> GetAllAsync() => await _context.Choses.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<ChoseUs> GetByIdAsync(int id) => await _context.Choses.FindAsync(id);
    }
}
