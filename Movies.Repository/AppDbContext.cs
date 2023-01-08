using Microsoft.EntityFrameworkCore;
using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

       public DbSet<Movie> Movies { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<MovieReview> MovieReviews { get; set; }
    }
}
