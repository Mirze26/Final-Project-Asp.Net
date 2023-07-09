using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class AboutInfoCreateVM
    {
        public string Description { get; set; }
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
