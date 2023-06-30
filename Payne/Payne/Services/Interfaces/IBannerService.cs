using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
    }
}
