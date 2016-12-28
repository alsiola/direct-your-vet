using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DYV.Controllers
{
    public class HowToController : Controller
    {
        // GET: /<controller>/
        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult DayLists()
        {
            return View();
        }
    }
}
