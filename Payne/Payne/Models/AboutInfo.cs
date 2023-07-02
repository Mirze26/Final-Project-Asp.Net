using System.ComponentModel.DataAnnotations.Schema;

namespace Payne.Models
{
    public class AboutInfo:BaseEntity
    {
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
