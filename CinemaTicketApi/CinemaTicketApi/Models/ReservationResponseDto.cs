namespace CinemaTicketApi.Models
{
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int ScreeningId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieImageUrl { get; set; }
        public string SeatNumbers { get; set; } 
    }
}
