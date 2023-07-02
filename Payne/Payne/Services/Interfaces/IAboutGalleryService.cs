using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IAboutGalleryService
    {
        Task<IEnumerable<AboutGallery>> GetAllAsync();
        Task<AboutGallery> GetByIdAsync(int id);
    }
}
