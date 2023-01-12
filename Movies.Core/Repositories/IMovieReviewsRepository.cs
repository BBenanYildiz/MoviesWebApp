using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Repositories
{
    public interface IMovieReviewsRepository : IGenericRepository<MovieReview>
    {
        List<MovieReview> GetMovieReviewWitByMovieId(int movie_id);
    }
}
