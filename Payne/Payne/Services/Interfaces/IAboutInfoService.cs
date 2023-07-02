using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IAboutInfoService
    {
        Task<IEnumerable<AboutInfo>> GetAllAsync();
        Task<AboutInfo> GetByIdAsync(int id);
    }
}
