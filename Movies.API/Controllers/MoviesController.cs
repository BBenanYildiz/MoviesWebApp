using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Service.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.Core.Services;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : CustomBaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IMovieReviewsService _movieReviewsService;
        public MoviesController(IConfiguration configuration,
            IMapper mapper,
            IMoviesService moviesService,
            IMovieReviewsService movieReviewsService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _moviesService = moviesService;
            _movieReviewsService = movieReviewsService;
        }

        /// <summary>
        /// Tüm filmleri getirir ve veritabanına insert işlemi gerçekleştirir.
        /// Bu kısımı göstermeyeceğiz
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            string apiKey = ApiKey();
            string baseUrl = BaseUrl();
            string apiUrl = baseUrl + "discover/movie?api_key=" + apiKey;

            return Ok();
        }

        [Route("get/{page}", Name = "GetMoviePage")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetMoviePage(int page)
        {
            string apiKey = ApiKey();
            string baseUrl = BaseUrl();
            string apiUrl = baseUrl + "discover/movie?api_key=" + apiKey + "&sort_by=popularity.desc&page=" + page;

            var response = WebHelper.Get(apiUrl);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);
            var rootDtos = _mapper.Map<List<MovieDTOs>>(myDeserializedClass.results.ToList());

            return CreateActionResult(CustomResponseDto<List<MovieDTOs>>.Success(200, rootDtos));
        }


        /// <summary>
        /// Film detayını getirir.
        /// </summary>
        /// <param name="id">Film Id No</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var result = _moviesService.GetDetail(id);
            //return StatusCode((int)result.StatusCode, result)
            return Ok();
        }

        /// <summary>
        /// Seçilen filme not ve puan ekleme yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post(int id, string note, int point)
        {
            string apiKey = ApiKey();
            string baseUrl = BaseUrl();

            string apiUrl = baseUrl + "movie/" + id + "?api_key=" + apiKey;
            var response = WebHelper.Get(apiUrl);
            var myDeserializedClass = JsonConvert.DeserializeObject<Movie>(response);

            var movieDetail = _mapper.Map<MovieDTOs>(myDeserializedClass);


            if (movieDetail != null)
            {
                MovieReview entity = new MovieReview();

                entity.Note = note;
                entity.Score = point;
                entity.MovieId = movieDetail.id;
                entity.UserId = 1;  // şimdilik bir 

                await _movieReviewsService.AddAsync(entity);
            }

            return CreateActionResult(CustomResponseDto<MovieDTOs>.Success(200, movieDetail));

        }

        /// <summary>
        /// Seçilen filmi e-posta olarak gönderir.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        [Route("{id}/mailAdress/{mailAdress}")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Shared(int id, string mailAdress)
        {
            var result = await _moviesService.SharedMail(id, mailAdress);
            return StatusCode((int)result.StatusCode, result);
        }

        //AppSetting Dosyasından Api Keyi Çekiyoruz.
        private string ApiKey()
        {
            var apiKey = _configuration.GetValue<string>("TmbdApiKey:api_key");
            return apiKey;
        }

        //AppSetting Dosyasından BaseUrl 'i Çekiyoruz.
        private string BaseUrl()
        {
            var baseUrl = _configuration.GetValue<string>("BaseUrl:base_url");
            return baseUrl;
        }

    }
}
