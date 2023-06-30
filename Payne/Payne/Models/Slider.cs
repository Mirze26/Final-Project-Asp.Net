﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Payne.Models
{
    public class Slider:BaseEntity
    {
        public string Offer { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
        public string BackgroundImage { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public IFormFile BackGroundPhoto { get; set; }

    }
}
