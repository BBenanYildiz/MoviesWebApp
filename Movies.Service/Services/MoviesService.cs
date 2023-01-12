using AutoMapper;
using Movies.Core.DTOs;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Core.Model.RequestModel;
using Movies.Core.Model.ResponseModel;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using Movies.Core.ViewModels;
using NLayerApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class MoviesService : GenericService<Movie>, IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        private readonly IMovieReviewsService _movieReviewsService;
        public MoviesService(IGenericRepository<Movie> repository,
            IUnitOfWork unitOfWork, IMoviesRepository moviesRepository, IMapper mapper,
            IMovieReviewsService movieReviewsService) : base(repository, unitOfWork)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
            _movieReviewsService = movieReviewsService;
        }

        /// <summary>
        /// Seçilen Filmi verilen mail adresine gönderir
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        public async Task<ApiResponse> SharedMail(int id, string mailAdress)
        {
            try
            {
                var validationIDResult = CustomValidation.IsValidID(id);
                if (!validationIDResult.IsValid)
                    throw new DirectoryNotFoundException(validationIDResult.Message);

                var validationEmailResult = CustomValidation.IsValidEmail(mailAdress);
                if (!validationEmailResult.IsValid)
                    throw new DirectoryNotFoundException(validationEmailResult.Message);

                var movieDetail = await this.GetByIdAsync(id);

                if (movieDetail is null)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                MailSendInformationModel mailInformation = new MailSendInformationModel
                {
                    Imbdpoint = movieDetail.vote_average.ToString(),
                    MovieDate = movieDetail.release_date,
                    MovieName = movieDetail.original_title,
                    MailAdress = mailAdress
                };

                var mailResult = MailHelper.SendMailInformation(mailInformation);
                if (!mailResult)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Seçilen Filmin Detayını, puanı ve kullanıcı yorumlarını getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetDetail(int id)
        {
            try
            {
                var validationIDResult = CustomValidation.IsValidID(id);
                if (!validationIDResult.IsValid)
                    throw new DirectoryNotFoundException(validationIDResult.Message);

                var movie = await GetByIdAsync(id);

                if (movie is null)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                var movieDetail = _mapper.Map<MovieDetailDTOs>(movie);
                var movieReivewDetail = _movieReviewsService.GetMovieReviewWitByMovieId(id);

                movieDetail.details = movieReivewDetail;

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, movieDetail);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Film listesini kayıt eder.
        /// </summary>
        /// <param name="moviesList"></param>
        /// <returns></returns>
        public async Task<Movie> InsertMovies(Root moviesList)
        {
            var rootDtos = _mapper.Map<List<MovieDTOs>>(moviesList.results.ToList());

            foreach (var item in rootDtos)
            {
                Movie entity = new Movie();

                entity.adult = item.adult;
                entity.backdrop_path = item.backdrop_path;
                entity.original_language = item.original_language;
                entity.original_title = item.original_title;
                entity.overview = item.overview;
                entity.popularity = item.popularity;
                entity.poster_path = item.poster_path;
                entity.release_date = item.release_date;
                entity.title = item.title;
                entity.video = item.video;
                entity.vote_average = item.vote_average;
                entity.vote_count = item.vote_count;

                return await AddAsync(entity);
            }

            return null;
        }

        /// <summary>
        /// Seçilen Filme yorum ve puan ekler.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Post(int id, MovieCommentAndPointRequestModel model)
        {
            try
            {
                var validationIDResult = CustomValidation.IsValidID(id);
                if (!validationIDResult.IsValid)
                    throw new DirectoryNotFoundException(validationIDResult.Message);

                var movieDetail = await this.GetByIdAsync(id);

                if (movieDetail is null)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                MovieReview entity = new MovieReview();

                entity.Note = model.Comment;
                entity.Score = model.Point;
                entity.MovieId = movieDetail.id;
                entity.UserId = 1;

                var resultInsert = await _movieReviewsService.AddAsync(entity);

                if (resultInsert is null)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, resultInsert);

            }
            catch (Exception)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.SuccessMessage);
            }
        }

    }
}
