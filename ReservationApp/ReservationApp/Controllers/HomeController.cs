using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using System.Security.Claims;

namespace ReservationApp.Controllers
{
    /// <summary>
    /// Controller for handling user actions related to reservations.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IReservationService _reservations;
        private readonly AppDbContext _context;
        private readonly ReservationValidationService _reservationValidationService;

        public HomeController(IReservationService reservations, AppDbContext context, ReservationValidationService reservationValidationService)
        {
            _reservations = reservations;
            _context = context;
            _reservationValidationService = reservationValidationService;
        }

        /// <summary>
        /// GET action for displaying the home page for user.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET action for displaying the create reservation view.
        /// </summary>
        /// <returns>The create reservation view with a list of available rooms in the ViewBag.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            var rooms = _context.Room.ToList();
            var model = new CompositeModel
            {
                roomsSelectList = new SelectList(rooms, "Id", "RoomNumber")
            };
            return View(model);
        }

        /// <summary>
        /// POST action for creating a new reservation.
        /// </summary>
        /// <param name="model">The composite model containing reservation data.</param>
        /// <returns>Returns a redirection to the Index action if the reservation is successfully created, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CompositeModel model)
        {
            var rooms = _context.Room.ToList();

            model.roomsSelectList = new SelectList(rooms, "Id", "RoomNumber");

            if (ModelState.IsValid)
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                model.reservation.ReceivedById = userIdClaim.Value;

                var selectedRoom = await _context.Room.FindAsync(model.reservation.RoomId);
                model.reservation.Room = selectedRoom;
                model.reservation.Price = selectedRoom.Price;

                if (!await _reservationValidationService.IsReservationDateValid(model.reservation))
                {
                    ModelState.AddModelError("reservation.Date", "No rooms are available for the selected period.");
                    return View(model);
                }

                model.reservation = await _reservations.CreateAsync(model.reservation);                

                // should redirect to page with all user's resrvations
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
