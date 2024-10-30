using Microsoft.AspNetCore.Mvc;
using MuoqaBackend.ToBD;
using MuoqaWeb.Models;
using System.Data;
using System.Diagnostics;

namespace MuoqaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UpdatePrice _updatePrice;

        public HomeController(ILogger<HomeController> logger, UpdatePrice updatePrice)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _updatePrice = updatePrice ?? throw new ArgumentNullException(nameof(updatePrice));
        }

        public IActionResult Index()
        {
            DataTable data =  _updatePrice.GetPrices();
            if(data != null)
                ViewBag.Data = data;
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
    }
}
