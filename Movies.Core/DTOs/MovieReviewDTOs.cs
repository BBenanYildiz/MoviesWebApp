using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DTOs
{
    public class MovieReviewDTOs
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Score { get; set; }
        public int Note { get; set; }
        public int UserId { get; set; }
    }
}
