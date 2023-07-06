namespace Payne.Areas.Admin.ViewModels
{
    public class SliderCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Offer { get; set; }
        public List<IFormFile> Photos { get; set; }
        public IFormFile BackGroundPhoto { get; set; }

    }
}
