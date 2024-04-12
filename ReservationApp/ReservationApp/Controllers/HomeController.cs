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
        private readonly IRoomService _rooms;
        private readonly ReservationValidationService _reservationValidationService;

        public HomeController(IReservationService reservations, ReservationValidationService reservationValidationService, IRoomService rooms)
        {
            _reservations = reservations;
            _reservationValidationService = reservationValidationService;
            _rooms = rooms;
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
            return View();
        }

        /// <summary>
        /// POST action for creating a new reservation.
        /// </summary>
        /// <param name="model">The reservation model containing reservation data.</param>
        /// <returns>Returns a redirection to the Index action if the reservation is successfully created, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Reservation model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                model.ReceivedById = userIdClaim.Value;

                var selectedRoom = _rooms.FindRoomByAvailableSlots(model.NumberOfPeople);
                if (selectedRoom is null)
                {
                    ModelState.AddModelError("reservation.NumberOfPeople", "No rooms to accommodate this number of people. Please split your reservation.");
                    return View(model);
                }
                model.Room = selectedRoom;
                model.Price = selectedRoom.Price;

                if (!await _reservationValidationService.IsReservationDateValid(model))
                {
                    ModelState.AddModelError("reservation.Date", "No rooms are available for the selected period.");
                    return View(model);
                }

                model = await _reservations.CreateAsync(model);

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("UserReservations");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UserReservations()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _reservations.FindReservationsByUserId(userIdClaim);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Reservation model, int id)
        {
            return View(_reservations.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Reservation model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                model.ReceivedById = userIdClaim.Value;

                var selectedRoom = _rooms.FindRoomByAvailableSlots(model.NumberOfPeople);
                if (selectedRoom is null)
                {
                    ModelState.AddModelError("reservation.NumberOfPeople", "No rooms to accommodate this number of people. Please split your reservation.");
                    return View(model);
                }
                model.Room = selectedRoom;
                model.Price = selectedRoom.Price;

                if (!await _reservationValidationService.IsReservationDateValid(model))
                {
                    ModelState.AddModelError("reservation.Date", "No rooms are available for the selected period.");
                    return View(model);
                }

                model = await _reservations.UpdateAsync(model);

                return RedirectToAction("UserReservations");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Cancel(int id)
        {
            return View(_reservations.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult>CancelConfirmed(int id)
        {
            var reservationId = _reservations.FindById(id);

            await _reservations.DeleteAsync(reservationId);

            return RedirectToAction("UserReservations");
        }

    }
}
