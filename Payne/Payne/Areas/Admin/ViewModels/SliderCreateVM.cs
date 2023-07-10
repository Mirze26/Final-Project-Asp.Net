namespace Payne.Areas.Admin.ViewModels
{
    public class SliderCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Offer { get; set; }

        public string Images { get; set; }
        public string BackGroundPhotos { get; set; }

        public IFormFile Photo { get; set; }
        public IFormFile BackGroundPhoto { get; set; }

    }
}
