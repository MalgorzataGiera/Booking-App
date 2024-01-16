using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using System.Diagnostics;

namespace ReservationApp.Controllers
{
    public class HomeController : Controller
    {
		private readonly IReservationService _reservations;
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(IReservationService reservations, ILogger<HomeController> logger, AppDbContext context)
        {
            _reservations = reservations;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Reservation> reservationsList = _reservations.FindAll();
            return View(reservationsList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int id)
        {
            return View(_reservations.FindById(id));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Statistics()
        {
            DateTime oneWeekAgo = DateTime.Now.AddDays(-7);

            int guestsThisWeek = _context.Reservations
                .Where(r => r.Date >= oneWeekAgo)
                .Sum(r => r.Id);

            ViewBag.GuestsThisWeek = guestsThisWeek;

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}