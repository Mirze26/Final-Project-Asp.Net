using System.ComponentModel.DataAnnotations;

namespace Payne.Areas.Admin.ViewModels
{
    public class BannerEditVM
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }

        public string Image { get; set; }

   
        public string Title { get; set; }

       
        public string Name { get; set; }

      

        public bool IsLarge { get; set; } = false;
    }
}
