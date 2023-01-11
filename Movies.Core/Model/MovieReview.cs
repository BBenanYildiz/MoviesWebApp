using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Model
{
    public class MovieReview
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public List<Movie> Movie { get; set; }
        public int Score { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public List<User> Users { get; set; }
    }
}
