using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
         public string Price { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Sku { get; set; }

        public int ColorId { get; set; }
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public List<IFormFile> Photos { get; set; }
    }
}
