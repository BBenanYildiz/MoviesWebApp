using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Model
{
    public class MovieReview:BaseEntity
    {
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
    }
}
