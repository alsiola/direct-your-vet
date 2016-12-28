using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DYV.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Technology()
        {
            return View();
        }

        public IActionResult Security()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Why()
        {
            return View();
        }
    }
}
