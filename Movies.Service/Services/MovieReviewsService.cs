using AutoMapper;
using Movies.Core.DTOs;
using Movies.Core.Model;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using NLayerApp.Core.Services;
using NLayerApp.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class MovieReviewsService : GenericService<MovieReview>, IMovieReviewsService
    {
        private readonly IMovieReviewsRepository _movieReviewRepository;
        private readonly IMapper _mapper;
        public MovieReviewsService(IGenericRepository<MovieReview> repository,
            IUnitOfWork unitOfWork, IMovieReviewsRepository movieReviewRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _movieReviewRepository = movieReviewRepository;
            _mapper = mapper;
        }

        public List<MovieReviewDetailsDTOs> GetMovieReviewWitByMovieId(int movie_id)
        {
            var movieReviews = _movieReviewRepository.GetMovieReviewWitByMovieId(movie_id);

            var movieReviewsDto = _mapper.Map<List<MovieReviewDetailsDTOs>>(movieReviews);

            return movieReviewsDto;
        }
    }
}
