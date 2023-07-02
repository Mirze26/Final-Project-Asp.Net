using Payne.Models;

namespace Payne.Services.Interfaces
{
    public interface IChoseUsService
    {
        Task<IEnumerable<ChoseUs>> GetAllAsync();
        Task<ChoseUs> GetByIdAsync(int id);
    }
}
