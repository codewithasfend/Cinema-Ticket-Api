using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketApi.Models
{
    [PrimaryKey(nameof(ReservationId), nameof(SeatId))] // Define composite key at the class level
    public class ReservationSeat
    {
        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
     
        public int SeatId { get; set; }

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }
    }
}
