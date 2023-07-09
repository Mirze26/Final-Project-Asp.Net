using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();

        Task<Brand> GetByIdAsync(int id);
    }
}
