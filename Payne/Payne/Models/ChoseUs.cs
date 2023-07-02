using System.ComponentModel.DataAnnotations.Schema;

namespace Payne.Models
{
    public class ChoseUs:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
