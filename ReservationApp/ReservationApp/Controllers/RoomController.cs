using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;

namespace ReservationApp.Controllers
{
    /// <summary>
    /// Controller for handling actions related to rooms, accessible only by Admin.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRoomService _rooms;

        public RoomController(AppDbContext context, IRoomService rooms)
        {
            _context = context;
            _rooms = rooms;
        }

        /// <summary>
        /// Displays a list of rooms with pagination.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>The view with the list of rooms.</returns>
        [HttpGet]
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
        public async Task<IActionResult> CreateRoom(Room model)
        {
            if (ModelState.IsValid)
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

