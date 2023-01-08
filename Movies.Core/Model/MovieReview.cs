namespace Movies.Core.Model
{
    public class MovieReview
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public ICollection<Movie> Movie { get; set; }
        public int Score { get; set; }
        public int Note { get; set; }
        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
