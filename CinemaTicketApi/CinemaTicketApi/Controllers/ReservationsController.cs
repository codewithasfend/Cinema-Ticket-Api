using CinemaTicketApi.Data;
using CinemaTicketApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ReservationsController : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public ReservationsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult> GetAllReservations()
        {
            var reservations = await dbContext.Reservations.ToListAsync();
            return Ok(reservations);
        }

        // Get Reservation By UserId
        [HttpGet("by-user/{userId}")]
        public IActionResult GetReservationByUserId(int userId)
        {
            var reservations = dbContext.Reservations
                                       .Where(r => r.UserId == userId)
                                       .Include(r => r.ReservationSeats)
                                       .ThenInclude(rs => rs.Seat)
                                       .Include(r => r.Screening) 
                                       .ThenInclude(s => s.Movie)
                                       .ToList();


            // Transform the reservations into the new DTO format
            var response = reservations.Select(r => new ReservationResponseDto
            {
                Id = r.Id,
                ReservationDate = r.ReservationDate,
                NumberOfSeats = r.NumberofSeats,
                MovieTitle = r.Screening.Movie.Title , 
                MovieImageUrl = r.Screening.Movie.ImageUrl ,
                Amount = r.Amount,
                UserId = r.UserId,
                ScreeningId = r.ScreeningId,
                SeatNumbers = string.Join(", ", r.ReservationSeats.Select(rs => $"{rs.Seat.Row}{rs.Seat.SeatNumber}"))
            }).ToList();

            return Ok(response);

           // return Ok(reservations);
        }



        /// Query Parameters: Use URL format with ?userId=1&screeningId=2&seatIds=3&seatIds=4&seatIds=5.
        // Form Data: Use "x-www-form-urlencoded" format in Postman.
        // JSON: For JSON data, you should update the method to accept a DTO and use[FromBody] to bind the JSON payload.

        [HttpGet("reserve")]
        public IActionResult ReserveSeats([FromQuery] int userId, [FromQuery] int screeningId, [FromQuery] List<int> seatIds)
        {
            try
            {
                // Fetch the screening to get the price per seat
                var screening = dbContext.Screenings.Include(s => s.Movie).FirstOrDefault(s => s.Id == screeningId);
                if (screening == null)
                {
                    return NotFound("Screening not found.");
                }

                // Fetch the seats with a join to check both screening ID and seat availability
                var seats = dbContext.Seats
                    .Where(s => seatIds.Contains(s.Id) && s.IsAvailable && s.ScreeningId == screeningId)
                    .ToList();


                // Check if the number of seats fetched matches the number of seat IDs provided
                if (seats.Count != seatIds.Count)
                {
                    return BadRequest("Some seats are not available."); // Return an error if any seats are already taken
                }

                // Calculate the number of seats and the total amount
                int numberOfSeats = seats.Count;
                int totalAmount = numberOfSeats * screening.PricePerSeat; // Multiply the number of seats by the price per seat

                // Create a new reservation object
                var reservation = new Reservation
                {
                    ReservationDate = DateTime.UtcNow,
                    UserId = userId,
                    ScreeningId = screeningId,
                    NumberofSeats = numberOfSeats, // Set the total number of seats reserved
                    Amount = totalAmount, // Set the total price
                    ReservationSeats = seats.Select(s => new ReservationSeat { SeatId = s.Id }).ToList()
                };

                // Mark each seat as unavailable 
                foreach (var seat in seats)
                {
                    seat.IsAvailable = false;
                }

                dbContext.Reservations.Add(reservation); // Add the reservation to the context
                dbContext.SaveChanges(); // Save changes to the database
                                      // Prepare the response object
                var response = new
                {
                    MovieId = screening.Movie.Id,          // Assuming Movie entity has an Id property
                    MovieName = screening.Movie.Title,      // Assuming Movie entity has a Name property
                    ScreeningDate = screening.ScreeningTime,        // Assuming Screening has a Date property
                    Seats = string.Join(",", seats.Select(s => s.Row + s.SeatNumber)),
                    Amount = totalAmount,
                };

                // Return the custom response
                return Ok(response);
               
            }
            catch 
            {
                return Conflict("Some seats were reserved by another user. Please try again."); // Return an error message
            }
        }
    }
}
