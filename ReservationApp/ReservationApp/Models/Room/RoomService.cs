using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Models
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoomAsync(Room room)
        {
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
        }

        public List<Room> FindAllRooms()
        {
            return _context.Room.ToList();
        }

        public Room? FindRoomById(int id)
        {
            return _context.Room.Find(id);
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            _context.Room.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Finds a room that can accommodate a given number of people.
        /// </summary>
        /// <param name="numberOfPeople">The number of people.</param>
        /// <returns>The room with the specified number of available slots (or greater), or null if not found.</returns>
        public Room? FindRoomByAvailableSlots(int numberOfPeople)
        {
            return _context.Room.FirstOrDefault(r => r.MaxPeopleNumber >= numberOfPeople);
        }

        /// <summary>
        /// Finds a page of reservations.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The page size.</param>
        /// <returns>A paging list of reservations.</returns>
        public PagingList<Room> FindPage(int page, int size)
        {
            return PagingList<Room>.Create(
                    (p, s) => _context.Room
                            .OrderBy(b => b.RoomNumber)
                            .Skip((p - 1) * size)
                            .Take(s)
                            .ToList(),
                    _context.Room.Count(),
                    page,
                    size);
        }
    }
}
