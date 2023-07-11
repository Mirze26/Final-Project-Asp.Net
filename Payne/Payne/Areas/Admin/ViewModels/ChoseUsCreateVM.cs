using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class ChoseUsCreateVM
    {
        public string Description { get; set; }
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
