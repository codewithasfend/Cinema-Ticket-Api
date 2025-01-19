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
    public class SeatsController : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public SeatsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Seat/1
        [HttpGet("{screeningId}")]
        public async Task<ActionResult> GetAllSeats(int screeningId)
        {
            //var seats = await dbContext.Seats.Where(s => s.ScreeningId == screeningId).ToListAsync();
            var seats = await dbContext.Seats
                       .Where(s => s.ScreeningId == screeningId)
                       .Select(s => new { s.Id, s.Row, s.SeatNumber, s.IsAvailable }) // Include only desired properties
                       .ToListAsync();
            return Ok(seats);
        }
    
    }
}
