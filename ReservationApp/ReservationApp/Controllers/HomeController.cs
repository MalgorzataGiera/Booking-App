using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System.Drawing;
using System.Security.Policy;

namespace ReservationApp.Controllers
{
    /// <summary>
    /// Controller for handling reservation actions.
    /// </summary>
    public class HomeController : Controller
    {
		private readonly IReservationService _reservations;
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRoomService _rooms;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="reservations">The reservation service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The database context.</param>
        public HomeController(IReservationService reservations, ILogger<HomeController> logger, AppDbContext context, UserManager<IdentityUser> userManager, IRoomService rooms)
        {
            _reservations = reservations;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _rooms = rooms;
        }

        /// <summary>
        /// GET action for displaying the index page with a paginated list of reservations.
        /// </summary>
        /// <param name="page">The page number to display (default is 1).</param>
        /// <param name="size">The number of items per page (default is 10).</param>
        /// <returns>The view with the paginated list of reservations.</returns>
        public IActionResult Index([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return View(_reservations.FindPage(page, size));
        }

        /// <summary>
        /// GET action for displaying the error page.
        /// </summary>
        /// <remarks>
        /// This action is called when an unhandled exception occurs in the application.
        /// It displays an error view with details about the exception.
        /// </remarks>
        /// <returns>The error view with details about the exception.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// GET action for displaying details of a reservation.
        /// </summary>
        /// <param name="id">The ID of the reservation.</param>
        /// <returns>The details view of the reservation.</returns>
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

        /// <summary>
        /// GET action for updating a reservation.
        /// </summary>
        /// <param name="id">The ID of the reservation.</param>
        /// <returns>The update view of the reservation.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Update(CompositeModel model, int id)
        {
            var rooms = _context.Room.ToList();
            model.roomsSelectList = new SelectList(rooms, "Id", "RoomNumber");
            model.reservation = _reservations.FindById(id);

            model.reservation.Id = id;

            return View(model);
        }

        /// <summary>
        /// POST action for updating a reservation.
        /// </summary>
        /// <remarks>
        /// This action updates a reservation in the system. 
        /// If the ModelState is valid, it sets the current user as the ReceivedBy for the reservation, 
        /// updates the reservation, and redirects to the Index action.
        /// If the ModelState is not valid, it returns the Update view with the model containing validation errors.
        /// </remarks>
        /// <param name="model">The reservation model to be updated.</param>
        /// <returns>If successful, redirects to the Index action; otherwise, returns the Update view with errors.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(CompositeModel model)
        {
			// Retrieve the list of rooms to populate the dropdown
			var rooms = _context.Room.ToList();
            model.roomsSelectList = new SelectList(rooms, "Id", "RoomNumber");

            if (ModelState.IsValid)
            {
                //Set the current user as the ReceivedBy for the reservation
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                model.reservation.ReceivedById = userIdClaim.Value;

                model.reservation.Room = await _context.Room.FindAsync(model.reservation.RoomId);

                model.reservation = await _reservations.UpdateAsync(model.reservation);

				return RedirectToAction("Index");
			}

			return View(model);
		}



		/// <summary>
		/// GET action for displaying the delete confirmation view.
		/// </summary>
		/// <param name="id">The ID of the reservation to be deleted.</param>
		/// <returns>The delete confirmation view.</returns>
		[HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            return View(_reservations.FindById(id));
        }

        /// <summary>
        /// POST action for confirming the deletion of a reservation.
        /// </summary>
        /// <remarks>
        /// This action deletes a reservation from the system. 
        /// It receives the ID of the reservation to be deleted, deletes it, 
        /// and redirects to the Index action.
        /// </remarks>
        /// <param name="id">The ID of the reservation to be deleted.</param>
        /// <returns>Redirects to the Index action after successful deletion.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationId = await _context.Reservations.FindAsync(id);

            await _reservations.DeleteAsync(reservationId);

            return RedirectToAction("Index");
        }

		/// <summary>
		/// GET action for displaying the create reservation view.
		/// </summary>
		/// <returns>The create reservation view with a list of available rooms in the ViewBag.</returns>
		[HttpGet]
        [Authorize]
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
        [Authorize]
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

                var res = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.RoomId == model.reservation.RoomId);
                var dateEnd = res.Date.AddDays(res.NumberOfNights).Date;
                var newResDateEnd = model.reservation.Date.AddDays(model.reservation.NumberOfNights).Date;

                var existingReservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.RoomId == model.reservation.RoomId && (r.Date.Date >= model.reservation.Date.Date && r.Date.Date <= newResDateEnd)
                    || (dateEnd >= model.reservation.Date.Date && dateEnd <= newResDateEnd));

                if (existingReservation != null)
                {
                    ModelState.AddModelError("reservation.RoomId", "The room is already reserved for this date.");
                    return View(model);
                }

                if (model.reservation.Date.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("reservation.Date", "Reservation date cannot be in the past.");
                    return View(model);
                }


                model.reservation = await _reservations.CreateAsync(model.reservation);
                var room = _rooms.FindRoomById(model.reservation.RoomId);
                room.Reservations.Add(model.reservation);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// GET action for displaying the statistics page.
        /// </summary>
        /// <returns>The statistics view with relevant data.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Statistics(string period = "last 7 days")
        {
            DateTime startDate;

            switch (period.ToLower())
            {
                case "last 7 days":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "this month":
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    break;
                case "this year":
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
                default:
                    startDate = DateTime.Now.AddDays(-7); break;
            }

            int reservationCount = _context.Reservations.Count(r => r.Date >= startDate);

            ViewBag.Period = period;
            ViewBag.GuestsCount = reservationCount;

            return View();
        }

        /// <summary>
        /// Displays a list of all users in the application.
        /// </summary>
        /// <returns>The view containing the list of users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        /// <summary>
        /// GET action for displaying the access denied page.
        /// </summary>
        /// <returns>The access denied view.</returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Displays a list of rooms with pagination.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>The view with the list of rooms.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RoomsList([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return View(_rooms.FindPage(page, size));
        }

        /// <summary>
        /// Displays the form to update a room.
        /// </summary>
        /// <param name="id">The id of the room to be updated.</param>
        /// <returns>The view with the form to update the room.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateRoom(int id)
        {
            var x = _rooms.FindRoomById(id);
            return View(_rooms.FindRoomById(id));
        }

        /// <summary>
        /// Updates the room.
        /// </summary>
        /// <param name="model">The room to be updated.</param>
        /// <returns>Redirects to the rooms list view after updating the room.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(Room model)
        {
            if (ModelState.IsValid)
            {
                
                model = await _rooms.UpdateRoomAsync(model);
                return RedirectToAction("RoomsList");
            }
            return View(model);            
        }

        /// <summary>
        /// Displays the form to create a new room.
        /// </summary>
        /// <returns>The view with the form to create a new room.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRoom()
        {
            return View();
        }

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="model">The room model containing room data.</param>
        /// <returns>Redirects to the rooms list view after creating the room.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom(Room model)
        {
            if(ModelState.IsValid)
            {
                var existingRoom = await _context.Room
                    .FirstOrDefaultAsync(r => r.RoomNumber == model.RoomNumber);

                if (existingRoom != null)
                {
                    ModelState.AddModelError("RoomNumber", "The room with that numer already exist.");
                    return View(model);
                }

                model = await _rooms.CreateRoomAsync(model);
                return RedirectToAction("RoomsList");
            }
            return View(model);
        }

    }
}