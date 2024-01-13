namespace ReservationApp.Models
{
    public class MemoryReservationService : IReservationService
    {
        private Dictionary<int, Reservation> _reservations = new Dictionary<int, Reservation>();
        public int Add(Reservation reservation)
        {
            int id = _reservations.Keys.Count != 0 ? _reservations.Keys.Max() : 0;
            reservation.Id = id + 1;
            _reservations.Add(reservation.Id, reservation);
            return reservation.Id;
        }

        public void Delete(int id)
        {
            _reservations.Remove(id);
        }

        public List<Reservation> FindAll()
        {
            return _reservations.Values.ToList();
        }

        public Reservation? FindById(int id)
        {
            return _reservations[id];
        }

        public void Update(Reservation reservation)
        {
            _reservations[reservation.Id] = reservation;
        }
    }
}
