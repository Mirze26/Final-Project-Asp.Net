using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Models;
using Payne.Services.Interfaces;

namespace Payne.Services
   
{
    public class AboutInfoService : IAboutInfoService
    {
        private readonly AppDbContext _context;

        public AboutInfoService (AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AboutInfo>> GetAllAsync() => await _context.AboutInfos.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<AboutInfo> GetByIdAsync(int id) => await _context.AboutInfos.FindAsync(id);
    }
}
