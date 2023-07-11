using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payne.Helpers.Enums;

namespace Payne.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
