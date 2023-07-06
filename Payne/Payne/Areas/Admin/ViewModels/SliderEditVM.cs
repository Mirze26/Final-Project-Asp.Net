using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class SliderEditVM
    {
        public IFormFile Photo { get; set; }
        [Required]
        public string Offer { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
