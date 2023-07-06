using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
    }
}
