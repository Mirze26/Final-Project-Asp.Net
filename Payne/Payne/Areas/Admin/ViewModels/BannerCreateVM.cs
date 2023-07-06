using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class BannerCreateVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsLarge { get; set; } = false;

        [Required]
        public IFormFile Photo { get; set; }
    }
}
