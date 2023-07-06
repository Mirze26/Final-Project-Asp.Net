using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payne.Models;
using Payne.Services;

namespace Payne.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet <AppUser> AppUsers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }
        public DbSet<ChoseUs> Choses { get; set; }
        public DbSet<AboutGallery> AboutGalleries { get; set; }
    }
}
