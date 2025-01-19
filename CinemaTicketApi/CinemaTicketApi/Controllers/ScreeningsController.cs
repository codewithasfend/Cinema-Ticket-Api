using CinemaTicketApi.Data;
using CinemaTicketApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ScreeningsController : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public ScreeningsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get Screening By Movie Id
        [HttpGet("by-movie/{movieId}")]
        public async Task<ActionResult> GetScreeningByMovieId(int movieId)
        {
            var screening = await dbContext.Screenings.Where(s => s.MovieId == movieId).ToListAsync();
            return Ok(screening);
        }

        // POST: api/Screenings
        [HttpPost]
        public async Task<ActionResult> PostScreening([FromBody] Screening screening)
        {
            dbContext.Screenings.Add(screening);
            await dbContext.SaveChangesAsync();

            // Generate Seats For This Screening

            GenerateSeatsForScreening(screening.Id);
            return StatusCode(StatusCodes.Status201Created);
        }

        public void GenerateSeatsForScreening(int screeningId)
        {
            int seatsPerRow = 9;
            int totalSeats = 81;
            int totalRows = (int)Math.Ceiling((double)totalSeats / seatsPerRow);

            var newSeats = new List<Seat>();

            for (int row = 0; row < totalRows; row++) // Start from 0 to use ASCII values
            {
                char rowLetter = (char)('A' + row); // Convert row index to letter (A, B, C, etc.)

                for (int seatNum = 1; seatNum <= seatsPerRow; seatNum++)
                {
                    int seatId = (row * seatsPerRow) + seatNum;
                    if (seatId >= totalSeats) break; // Ensure we don't exceed total seats

                    var seat = new Seat
                    {
                        Row = rowLetter.ToString(), // Use the letter for the row
                        SeatNumber = seatNum,
                        IsAvailable = true,
                        ScreeningId = screeningId // Associate seats with the screening
                    };

                    newSeats.Add(seat);
                }
            }

            dbContext.Seats.AddRange(newSeats);
            dbContext.SaveChanges();
        }


        // DELETE: api/Screening/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            var screening = await dbContext.Screenings.FindAsync(id);
            if (screening == null)
            {
                return NotFound();
            }
            dbContext.Screenings.Remove(screening);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
