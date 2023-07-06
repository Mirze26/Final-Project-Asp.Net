using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class BannerEditVM
    {
        public IFormFile Photo { get; set; }

        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Name { get; set; }

        public int Id { get; set; }

        public bool IsLarge { get; set; } = false;
    }
}
