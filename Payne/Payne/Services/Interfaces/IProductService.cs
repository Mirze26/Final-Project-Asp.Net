using Payne.Models;
using System.Reflection.Metadata;

namespace Payne.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);

        Task<Product> GetFullDataByIdAsync(int id);
        Task<IEnumerable<Product>> GetPaginatedDatas(int page, int take);

        Task<int> GetCountAsync();
    }
}
