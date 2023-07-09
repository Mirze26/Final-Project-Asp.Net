using Payne.Models;

namespace Payne.Areas.Admin.ViewModels
{
    public class ProductEditVM
    {
      
        public string Name { get; set; }
  
        public string Description { get; set; }

        public string Price { get; set; }
       
        public int Count { get; set; }
  
        public int Rating { get; set; }
      
        public string Sku { get; set; }

        public int ColorId { get; set; }

        public int BrandId { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }

    }
}
