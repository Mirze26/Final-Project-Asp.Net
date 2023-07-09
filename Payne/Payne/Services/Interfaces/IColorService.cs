using Microsoft.EntityFrameworkCore;
using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllAsync();

        Task<Color> GetByIdAsync(int id);
    }
}
