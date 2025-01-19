namespace CinemaTicketApi.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime ScreeningTime { get; set; }
        public int PricePerSeat { get; set; }   
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
