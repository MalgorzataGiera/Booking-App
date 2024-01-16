using Microsoft.AspNetCore.Mvc;
using ReservationApp.Models;
using System.Diagnostics;

namespace ReservationApp.Controllers
{
    public class HomeController : Controller
    {
		private readonly IReservationService _reservations;
		private readonly ILogger<HomeController> _logger;

        public HomeController(IReservationService reservations, ILogger<HomeController> logger)
        {
            _reservations = reservations;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Reservation> reservationsList = _reservations.FindAll();
            return View(reservationsList);
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