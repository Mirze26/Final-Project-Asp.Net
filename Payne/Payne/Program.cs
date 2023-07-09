using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payne.Data;
using Payne.Services.Interfaces;
using Payne.Services;
using Payne.Models;

using Payne.Helpers.AccountSetting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); /// bunu login registr ucun yazriq


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8; // bu paswordun lensi en azi 8 olmalidir yeni girilen sifreye en azi 8 simvol daxil elemlidir
    options.Password.RequireDigit = true; // passworda reqem mutleq sekilde olsun
    options.Password.RequireLowercase = true; // balaca herifler mutlreq sekilde olsun
    options.Password.RequireUppercase = true; // boyuk herif en azi 1 dene olsun
    options.Password.RequireNonAlphanumeric = true;  // simvolar en azi 1 dene oslun yeni herif ve reqemden basqa  altdan xet meselcun noqte ve s

    options.User.RequireUniqueEmail = true; // her istifadeci ucun bir emale olmalidir yeni bir emailden 2 istifadeci istifade edib registir ola bilmez
    options.SignIn.RequireConfirmedEmail = true;  /// bunu yazanda emila mesaj gedirki tesdiqle

    options.Lockout.MaxFailedAccessAttempts = 3; // 3 defe   logini tekrar tekerar kece  biler en azi 3 defe sehv ede biler

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);  // bu ise 2 defe sehv edenden sonra bloklayir 30 deqiqelik

    options.Lockout.AllowedForNewUsers = true; // bu ise odurki yeni registerden kecen adam en azi 1 defe login olmalidir yuxarida yazilanlar ona ait deyil yeni sehv ede biler

});




builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();
builder.Services.AddScoped<IAboutInfoService, AboutInfoService>();
builder.Services.AddScoped<IChoseUsService, ChoseUsService>();
builder.Services.AddScoped<IAboutGalleryService, AboutGalleryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailService, EmailService>(); // email confrim ucundur

builder.Services.AddScoped<EmailSettings>();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("");
}


app.UseHttpsRedirection();

app.UseStaticFiles();



app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();




app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
