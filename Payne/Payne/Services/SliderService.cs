using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class SliderService : ISliderService
    {

        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Slider>> GetAllAsync() => await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Slider> GetFullDataByIdAsync(int id) => await _context.Sliders.FindAsync(id);
    }
}
