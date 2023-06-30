using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();

        Task<Slider> GetFullDataByIdAsync(int id);
    }
}
