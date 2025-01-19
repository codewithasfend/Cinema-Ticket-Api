using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CinemaTicketApi.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string Row { get; set; }
        public int SeatNumber { get; set; } 
        public bool IsAvailable { get; set; }

        [ForeignKey("ScreeningId")]
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }

        [JsonIgnore]
        public ICollection<ReservationSeat> ReservationSeats { get; set; }
    }
}
