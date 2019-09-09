using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IslaamAdmin.Controllers
{
    public class AppDataController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
