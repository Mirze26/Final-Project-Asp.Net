using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class AboutInfoEditVM
    {
        public string Description { get; set; }
        public string Title { get; set; }

        public IFormFile Photo { get; set; }
    }
}
