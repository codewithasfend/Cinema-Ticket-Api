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

    public class MoviesController : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public MoviesController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<object>> GetAllMovies(string movieType)
        {
            //var movies =  await dbContext.Movies.ToListAsync();
            //return Ok(movies);


            var moviesQuery = dbContext.Movies.AsQueryable();
            if (movieType == "nowplaying")
            {
                moviesQuery = moviesQuery.Where(m => m.Type == "nowplaying");
            }
            else if (movieType == "trending")
            {
                moviesQuery = moviesQuery.Where(m => m.Type == "trending");

            }
            else if (movieType == "latest")
            {
                moviesQuery = moviesQuery.Where(m => m.Type == "latest");

            }
            else
            {
                throw new ArgumentException("Invalid movie type");
            }

            var movies = await moviesQuery.Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                ImageUrl = m.ImageUrl,
              
            }).ToListAsync();

            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovie(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult> PostMovie([FromBody] Movie movie)
        {
            dbContext.Movies.Add(movie);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await dbContext.Movies
                .Include(m => m.Screenings) // Include related screenings
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Delete associated screenings and their seats
            foreach (var screening in movie.Screenings)
            {
                dbContext.Seats.RemoveRange(screening.Seats);
            }

            dbContext.Screenings.RemoveRange(movie.Screenings);
            dbContext.SaveChanges();

            return NoContent();
        }

    }
}
