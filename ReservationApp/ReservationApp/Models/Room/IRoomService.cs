namespace ReservationApp.Models
{
    public interface IRoomService
    {
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(Room room);
        Room? FindRoomById(int id);
        List<Room> FindAllRooms();
        Room? FindRoomByAvailableSlots(int numberOfPeople);
        PagingList<Room> FindPage(int page, int size);
    }
}
