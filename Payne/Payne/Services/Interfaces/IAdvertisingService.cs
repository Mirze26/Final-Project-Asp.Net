using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<IEnumerable<Advertising>> GetAllAsync();

        Task<Advertising> GetFullDataByIdAsync(int id);
    }
}
