using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using System.Diagnostics;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var reservation = _context.Reservations
        .Include(r => r.ReceivedBy)
        .FirstOrDefault(r => r.Id == id);

            CompositeModel compositeModel = new CompositeModel
            ();
            compositeModel.reservation = _reservations.FindById(id);
            compositeModel.userName = reservation.ReceivedBy.UserName;
            return View(compositeModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int id)
        {
            return View(_reservations.FindById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Reservation model)
        {
            if(ModelState.IsValid)
            {
                //Set the current user as the ReceivedBy for the reservation
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                model.ReceivedById = userIdClaim.Value;

                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(_reservations.FindById(model.Id));
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
                
        [HttpPost]

        [Authorize]
        public async Task<IActionResult> Create(Reservation model)
        {
            if (ModelState.IsValid)
            {
                //Set the current user as the ReceivedBy for the reservation
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                model.ReceivedById = userIdClaim.Value;

                _context.Reservations.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");               
            }
            
            return View(model);
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