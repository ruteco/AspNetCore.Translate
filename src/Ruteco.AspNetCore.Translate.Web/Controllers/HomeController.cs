using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate.Web.Models;

namespace Ruteco.AspNetCore.Translate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITranslationService _translationService;

        public HomeController(ITranslationService translation)
        {
            _translationService = translation;
        }

        public IActionResult Index([FromQuery]string lang = "en")
        {
            ViewBag.translation = _translationService;
            ViewBag.lang = lang;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(_translationService.Get("en","Hello"));
        }


    }
}
