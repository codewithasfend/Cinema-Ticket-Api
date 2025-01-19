using CinemaTicketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationSeat> ReservationSeats { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Andrew", Email = "andrew@email.com", Password = "And@1234" },
                new User { Id = 2, Name = "Bob", Email = "bob@email.com", Password = "Bb@1234" },
                new User { Id = 3, Name = "John", Email = "john@email.com", Password = "Jn@1234" },
                new User { Id = 4, Name = "Chris", Email = "chris@email.com", Password = "Crs@1234" }
            );

            // Seed data for Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Description = "Harry Potter, an orphaned boy, discovers that he is a wizard and attends Hogwarts School of Witchcraft and Wizardry. With the help of his friends, Ron and Hermione, he battles the dark forces led by Lord Voldemort, the dark wizard who killed his parents. As he navigates his first year at school, he learns about magic, makes new friends, and faces challenges that test his bravery and loyalty. The story explores themes of friendship, courage, and the battle between good and evil.",
                    ReleaseDate = new DateTime(2001, 11, 16),
                    Type = "nowplaying",
                    Duration = "2h 32m",
                    ImageUrl = "harrypotter.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    Description = "In a world of magic and danger, a young hobbit named Frodo Baggins inherits a powerful ring. He sets off on a quest to destroy it in the fires of Mount Doom, accompanied by a fellowship of diverse characters including elves, dwarves, and men. As they journey through Middle-earth, they face numerous challenges and battles against the dark lord Sauron, who seeks to reclaim the ring. The story delves into themes of friendship, sacrifice, and the struggle between light and darkness.",
                    ReleaseDate = new DateTime(2001, 12, 19),
                    Type = "latest",
                    Duration = "2h 58m",
                    ImageUrl = "lordoftherings.jpg"
                },
                new Movie
                {
                    Id = 3,
                    Title = "Inception",
                    Description = "Dom Cobb is a skilled thief who specializes in extracting secrets from deep within the subconscious during the dream state. When he is offered a chance to have his past crimes forgiven, he must perform the impossible: planting an idea into someone's mind, a process known as inception. To accomplish this, he assembles a team of specialists, and they embark on a mind-bending journey through layered dreams, battling enemies and navigating through their own fears and insecurities. The film explores complex themes of reality, dreams, and the nature of consciousness.",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Type = "trending",
                    Duration = "2h 28m",
                    ImageUrl = "inception.jpg"
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Matrix",
                    Description = "In a dystopian future, a computer hacker named Neo discovers the shocking truth: reality as he knows it is a simulated world created by machines that have enslaved humanity. With the help of rebels led by Morpheus and Trinity, Neo embarks on a quest to free humanity from the Matrix. Along the way, he learns about his true potential, confronts powerful adversaries, and ultimately faces the enigmatic Agent Smith. The film raises profound questions about reality, free will, and the nature of existence.",
                    ReleaseDate = new DateTime(1999, 3, 31),
                    Type = "latest",
                    Duration = "2h 16m",
                    ImageUrl = "matrix.jpg"
                },
                new Movie
                {
                    Id = 5,
                    Title = "The Avengers",
                    Description = "When an alien threat looms over Earth, Nick Fury, the director of S.H.I.E.L.D., assembles a team of superheroes to protect humanity. This group includes Iron Man, Thor, Captain America, the Hulk, Black Widow, and Hawkeye. As they face internal conflicts and external battles against the powerful Loki and his alien army, the heroes must learn to work together to save the world. The film showcases action-packed sequences, witty banter, and the significance of teamwork in overcoming adversity.",
                    ReleaseDate = new DateTime(2012, 5, 4),
                    Type = "nowplaying",
                    Duration = "2h 23m",
                    ImageUrl = "avengers.jpg"
                },
                new Movie
                {
                    Id = 6,
                    Title = "Interstellar",
                    Description = "In the face of an impending ecological disaster, a group of astronauts embarks on a journey through a wormhole in search of a new habitable planet. Led by former NASA pilot Cooper, the team faces the challenges of time dilation, gravity, and their emotional struggles as they grapple with the fate of humanity. The film explores deep themes of love, sacrifice, and the connection between time and space, all set against a visually stunning backdrop of distant galaxies.",
                    ReleaseDate = new DateTime(2014, 11, 7),
                    Type = "trending",
                    Duration = "2h 49m",
                    ImageUrl = "interstellar.jpg"
                },
                new Movie
                {
                    Id = 7,
                    Title = "The Dark Knight",
                    Description = "In Gotham City, Batman faces his greatest challenge yet with the emergence of the Joker, a criminal mastermind who seeks to unleash chaos and anarchy. As the Joker wreaks havoc, Batman must confront his own moral dilemmas and the fine line between heroism and vigilantism. With the help of Commissioner Gordon and District Attorney Harvey Dent, Batman battles not only the Joker but also his inner demons. The film delves into themes of justice, morality, and the impact of choices on society.",
                    ReleaseDate = new DateTime(2008, 7, 18),
                    Type = "latest",
                    Duration = "2h 32m",
                    ImageUrl = "darkknight.jpg"
                },
                new Movie
                {
                    Id = 8,
                    Title = "Jurassic Park",
                    Description = "In a groundbreaking amusement park, genetically engineered dinosaurs roam freely, drawing in visitors from around the world. When a group of experts is invited to assess the park's safety, things take a deadly turn as the dinosaurs escape their confines. Chaos ensues as the characters must fight for survival against these magnificent yet terrifying creatures. The film explores themes of scientific ethics, nature's unpredictability, and the consequences of playing God.",
                    ReleaseDate = new DateTime(1993, 6, 11),
                    Type = "nowplaying",
                    Duration = "2h 7m",
                    ImageUrl = "jurassicpark.jpg"
                },
                new Movie
                {
                    Id = 9,
                    Title = "The Lion King",
                    Description = "In the heart of the African savannah, a young lion cub named Simba learns about responsibility and courage as he navigates the challenges of growing up. After the tragic death of his father, Mufasa, at the hands of his uncle Scar, Simba flees, believing he is to blame. However, with the help of friends Timon and Pumbaa, he learns to embrace his true identity and return to reclaim his rightful place as king. The film is a timeless tale of family, bravery, and redemption.",
                    ReleaseDate = new DateTime(1994, 6, 15),
                    Type = "trending",
                    Duration = "1h 58m",
                    ImageUrl = "lionking.jpg"
                },
                new Movie
                {
                    Id = 10,
                    Title = "Finding Nemo",
                    Description = "In the vast ocean, Marlin, a clownfish, embarks on a perilous journey to find his son, Nemo, who has been captured by a diver. Along the way, he encounters various sea creatures, each with their own unique personality and challenges. With the help of Dory, a forgetful fish, Marlin learns to overcome his fears and embrace the adventure of life. The film beautifully illustrates themes of family, friendship, and the importance of letting go.",
                    ReleaseDate = new DateTime(2003, 5, 30),
                    Type = "latest",
                    Duration = "1h 40m",
                    ImageUrl = "findingnemo.jpg"
                },
                new Movie
                {
                    Id = 11,
                    Title = "Frozen",
                    Description = "In the kingdom of Arendelle, Princess Anna sets off on a quest to find her sister Elsa, whose icy powers have trapped their land in eternal winter. Along the way, she teams up with rugged ice harvester Kristoff and his loyal reindeer Sven. Together, they face formidable challenges as they navigate treacherous terrain and battle against Elsa's fears. The film is a heartwarming tale of sisterly love and the importance of self-acceptance, featuring memorable songs and stunning animation.",
                    ReleaseDate = new DateTime(2013, 11, 27),
                    Type = "trending",
                    Duration = "1h 42m",
                    ImageUrl = "frozen.jpg"
                },
                new Movie
                {
                    Id = 12,
                    Title = "Toy Story",
                    Description = "In a world where toys come to life, Woody, a pull-string cowboy doll, feels threatened by the arrival of Buzz Lightyear, a space ranger action figure. When the two toys are accidentally left behind during a family move, they must set aside their differences and work together to find their way home. The film is a charming exploration of friendship, loyalty, and the importance of embracing one's identity, filled with humor and heartwarming moments.",
                    ReleaseDate = new DateTime(1995, 11, 22),
                    Type = "latest",
                    Duration = "1h 21m",
                    ImageUrl = "toystory.jpg"
                },
                new Movie
                {
                    Id = 13,
                    Title = "Spider-Man: Into the Spider-Verse",
                    Description = "Teenager Miles Morales becomes the Spider-Man of his reality and crosses paths with five counterparts from another dimensions to stop a threat to all realities. Together, they must embrace their powers and learn what it truly means to be a hero. The film is a visually stunning and innovative take on the superhero genre, exploring themes of identity, responsibility, and the power of teamwork.",
                    ReleaseDate = new DateTime(2018, 12, 14),
                    Type = "nowplaying",
                    Duration = "1h 57m",
                    ImageUrl = "spiderman.jpg"
                },
                new Movie
                {
                    Id = 14,
                    Title = "Shrek",
                    Description = "An ogre named Shrek embarks on a quest to rescue Princess Fiona, who is held captive by a dragon. Along the way, he encounters various fairy-tale characters and learns about friendship, love, and acceptance. The film cleverly subverts traditional fairy-tale tropes, providing humor and heart in equal measure, making it a beloved classic for audiences of all ages.",
                    ReleaseDate = new DateTime(2001, 5, 22),
                    Type = "trending",
                    Duration = "1h 30m",
                    ImageUrl = "shrek.jpg"
                },
                new Movie
                {
                    Id = 15,
                    Title = "The Incredibles",
                    Description = "In a world where superheroes are forced to hide their powers, Bob Parr, also known as Mr. Incredible, longs for the days of saving the world. When a mysterious villain threatens the safety of the city, Bob and his family must come together to embrace their abilities and fight against evil. The film is a thrilling adventure filled with action, humor, and valuable lessons about family and self-acceptance.",
                    ReleaseDate = new DateTime(2004, 11, 5),
                    Type = "latest",
                    Duration = "1h 55m",
                    ImageUrl = "incredibles.jpg"
                }
            );

            // Seed Screenings
            var screenings = new List<Screening>();
            int screeningIdCounter = 1;

            for (int movieId = 1; movieId <= 15; movieId++)
            {
                screenings.Add(new Screening { Id = screeningIdCounter++, ScreeningTime = DateTime.Now.AddHours(2), PricePerSeat = 10, MovieId = movieId });
                screenings.Add(new Screening { Id = screeningIdCounter++, ScreeningTime = DateTime.Now.AddHours(5), PricePerSeat = 12, MovieId = movieId });
                screenings.Add(new Screening { Id = screeningIdCounter++, ScreeningTime = DateTime.Now.AddHours(8), PricePerSeat = 15, MovieId = movieId });
            }

            modelBuilder.Entity<Screening>().HasData(screenings);

            // Generate and Seed Seats
            var seats = GenerateSeatsForSeededScreenings(screenings.Select(s => s.Id).ToArray());
            modelBuilder.Entity<Seat>().HasData(seats);


        }

        // Method to Generate Seats for All Screenings During Seeding
        private List<Seat> GenerateSeatsForSeededScreenings(int[] screeningIds)
        {
            int seatsPerRow = 9;
            int totalSeats = 81; // Total seats per screening
            int totalRows = (int)Math.Ceiling((double)totalSeats / seatsPerRow);

            var allSeats = new List<Seat>();
            int seatIdCounter = 1;

            foreach (var screeningId in screeningIds)
            {
                for (int row = 0; row < totalRows; row++)
                {
                    char rowLetter = (char)('A' + row);

                    for (int seatNum = 1; seatNum <= seatsPerRow; seatNum++)
                    {
                        var seat = new Seat
                        {
                            Id = seatIdCounter++,
                            Row = rowLetter.ToString(),
                            SeatNumber = seatNum,
                            IsAvailable = true,
                            ScreeningId = screeningId
                        };

                        allSeats.Add(seat);
                    }
                }
            }

            return allSeats;
        }
    }

}
