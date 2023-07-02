using Payne.Models;

namespace Payne.ViewModels.About
{
    public class AboutVM
    {
        public IEnumerable<AboutInfo> AboutInfos { get; set; }
        public IEnumerable<AboutGallery> AboutGalleries { get; set; }
        public IEnumerable<ChoseUs> Choses { get; set; }
      
    }
}
