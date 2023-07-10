using Payne.Models;

namespace Payne.ViewModels.Shop
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int Rating { get; set; }

        public string Sku { get; set; }

        public string ColorName { get; set; }

        public string BrandName { get; set; }

        public ICollection<ProductImage> Images { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
