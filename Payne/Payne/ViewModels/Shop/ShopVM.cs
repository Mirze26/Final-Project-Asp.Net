using Payne.Models;

namespace Payne.ViewModels.Shop
{
    public class ShopVM
    {
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
