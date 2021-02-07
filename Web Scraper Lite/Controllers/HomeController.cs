using Feature.ContentScrap;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebScraperLite.Models;

namespace WebScraperLite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContentParser _contentParser;

        public HomeController(ILogger<HomeController> logger, IContentParser contentParser)
        {
            _logger = logger;
            _contentParser = contentParser;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(StatsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            model.Images = _contentParser.GetImages(model.Url);
            model.documentStats = _contentParser.GetDocumentStats(model.Url);
            return View(model);
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
    }
}
