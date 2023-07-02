using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
{
    public class AboutGalleryService : IAboutGalleryService
    {
        private readonly AppDbContext _context;

        public AboutGalleryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AboutGallery>> GetAllAsync() => await _context.AboutGalleries.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<AboutGallery> GetByIdAsync(int id) => await _context.AboutGalleries.FindAsync(id);
    }
}
