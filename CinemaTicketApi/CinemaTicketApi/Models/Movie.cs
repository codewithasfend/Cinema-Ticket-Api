namespace CinemaTicketApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }    



        public ICollection<Screening> Screenings { get; set; }
    }
}
