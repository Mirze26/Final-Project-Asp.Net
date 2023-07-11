using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payne.Helpers.Enums;

namespace Payne.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
