namespace ReservationApp.Models
{
    public class MemoryReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public MemoryReservationService(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public int Add(Reservation reservation)
        //{
        //    int id = _reservations.Keys.Count != 0 ? _reservations.Keys.Max() : 0;
        //    reservation.Id = id + 1;
        //    _reservations.Add(reservation.Id, reservation);
        //    return reservation.Id;
        //}

        //public void Delete(int id)
        //{
        //    _reservations.Remove(id);
        //}

        public List<Reservation> FindAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation? FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        //public Reservation? FindById(int id)
        //{
        //    return _reservations[id];
        //}

        //public void Update(Reservation reservation)
        //{
        //    _reservations[reservation.Id] = reservation;
        //}
    }
}
