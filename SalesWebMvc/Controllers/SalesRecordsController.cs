using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch() // Busca simples
        {
            return View();
        }

        public IActionResult GroupingSearch() // Busca agrupada
        {
            return View();
        }
    }
}
