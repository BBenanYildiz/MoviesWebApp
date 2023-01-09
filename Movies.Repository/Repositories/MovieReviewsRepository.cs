using Microsoft.EntityFrameworkCore;
using Movies.Core.Model;
using Movies.Core.Repositories;
using Movies.Repository;
using Movies.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{
    public class MovieReviewsRepository : GenericRepository<MovieReview>, IMovieReviewsRepository
    {
        public MovieReviewsRepository(AppDbContext context) : base(context)
        {
        }
      
    }
}
