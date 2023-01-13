using Microsoft.EntityFrameworkCore;
using Movies.Core.Model;
using Movies.Repository.Initi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasKey(c => new { c.id });

            modelBuilder.Entity<User>()
              .HasKey(c => new { c.Id });

            modelBuilder.Entity<MovieReview>()
             .HasKey(c => new { c.Id });

            new DbInitializer(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
    }
}
