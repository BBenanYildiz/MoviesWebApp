﻿using AutoMapper;
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
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, validationIDResult.Message);

                var validationEmailResult = CustomValidation.IsValidEmail(mailAdress);
                if (!validationEmailResult.IsValid)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, validationEmailResult.Message);

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
                MailHelper.SystemInformationMail(ex.ToString(), "SharedMail / İşlem Sırasında Hata Meydana Geldi.");
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
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
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, validationIDResult.Message);

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
                MailHelper.SystemInformationMail(ex.ToString(), "GetDetail / İşlem Sırasında Hata Meydana Geldi.");
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage); ;
            }
        }

        /// <summary>
        /// Film listesini kayıt eder.
        /// </summary>
        /// <param name="moviesList"></param>
        /// <returns></returns>
        public async Task<int> InsertMovies(Root moviesList)
        {
            try
            {
                //Burayı Test etmelisin her seferinde aynı filmleri database kayıt edersek sıkıntı yaşarız.
                //Var olanı kontrol edip güncellememiz gerekli. olmayan varsa onu kayıt edicez.
                Movie entity = null;
                bool newRecord = false;

                foreach (var item in moviesList.results)
                {
                    entity = GetMovieDetailWithByTitle(item.original_title);

                    if (entity is null)
                    {
                        newRecord = true;
                        entity = new Movie();
                    }

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

                    if (newRecord)
                        await this.AddAsync(entity);
                    else
                    {
                        entity.id = item.id;
                        await this.UpdateAsync(entity);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                MailHelper.SystemInformationMail(ex.ToString(), "InsertMovies / İşlem Sırasında Hata Meydana Geldi.");
                return 0;
            }
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
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, validationIDResult.Message);

                var validationPointResult = CustomValidation.IsValidPoint(model.Point);
                if (!validationPointResult.IsValid)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, message: validationPointResult.Message);

                var validationCommentResult = CustomValidation.IsValidComment(model.Comment);
                if (!validationCommentResult.IsValid)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, message: validationCommentResult.Message);

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
            catch (Exception ex)
            {
                MailHelper.SystemInformationMail(ex.ToString(), "Post / İşlem Sırasında Hata Meydana Geldi.");
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public Movie GetMovieDetailWithByTitle(string orjinal_title)
        {
            return _moviesRepository.GetMovieDetailWithByTitle(orjinal_title);
        }
    }
}
