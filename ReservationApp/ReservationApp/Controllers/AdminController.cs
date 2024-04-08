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
    /// Controller for handling reservation actions, accessible only by Admin.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IReservationService _reservations;
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRoomService _rooms;
        
        public AdminController(IReservationService reservations, ILogger<AdminController> logger, AppDbContext context, UserManager<IdentityUser> userManager, IRoomService rooms)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // you cannot delete admin user
            var reservationId = await _context.Reservations.FindAsync(id);

            await _reservations.DeleteAsync(reservationId);

            return RedirectToAction("Index");
        }

        

        /// <summary>
        /// GET action for displaying the statistics page.
        /// </summary>
        /// <returns>The statistics view with relevant data.</returns>
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

    }
}