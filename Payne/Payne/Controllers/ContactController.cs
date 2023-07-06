using Microsoft.AspNetCore.Mvc;

namespace Payne.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
