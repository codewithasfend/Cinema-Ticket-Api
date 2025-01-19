namespace CinemaTicketApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }

        public int NumberofSeats { get; set; }
        public int Amount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }

        public ICollection<ReservationSeat> ReservationSeats { get; set; }
    }
}
