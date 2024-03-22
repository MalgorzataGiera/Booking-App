namespace ReservationApp.Models
{
    public interface IRoomService
    {
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(Room room);
        Room? FindRoomByIdAsync(int id);
        List<Room> FindAllRooms();
        PagingList<Room> FindPage(int page, int size);
    }
}
