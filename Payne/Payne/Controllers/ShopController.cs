﻿using Microsoft.AspNetCore.Mvc;

namespace Payne.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            return View(id);
        }
    }
}
