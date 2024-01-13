namespace ReservationApp.Models
{
    public interface IReservationService
    {
        int Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);        
        List<Reservation> FindAll();
        Reservation? FindById(int id);
    }
}
