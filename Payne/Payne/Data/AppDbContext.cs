using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payne.Models;

namespace Payne.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet <AppUser> AppUsers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }
        public DbSet<ChoseUs> Choses { get; set; }
        public DbSet<AboutGallery> AboutGalleries { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

    }
}
