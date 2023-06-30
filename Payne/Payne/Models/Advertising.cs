using System.ComponentModel.DataAnnotations.Schema;

namespace Payne.Models
{
    public class Advertising:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
