using Payne.Models;

namespace Payne.Areas.Admin.ViewModels
{
    public class ProductDetailVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public int Count { get; set; }

        public int Rating { get; set; }

        public string Sku { get; set; }

        public string ColorName { get; set; }

        public string BrandName { get; set; }

        public ICollection<ProductImage> Images { get; set; }
    }
}
