﻿using Payne.Models;

namespace Payne.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Advertising> Advertisings { get; set; }
        public IEnumerable<Product> Products { get; set; }

        
    }
}
