using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Core.Model.RequestModel;
using Movies.Service.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayerApp.Core.Services;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;

namespace Movies.API.Controllers
{ 

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
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
        /// Tüm Filmleri Getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var result = await _moviesService.Get();
            return StatusCode((int)result.StatusCode, result);
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
            var result = await _moviesService.GetDetail(id);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Seçilen filme not ve puan ekleme yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieCommentAndPointRequestModel"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post(int id, MovieCommentAndPointRequestModel movieCommentAndPointRequestModel)
        {
            var result = await _moviesService.Post(id, movieCommentAndPointRequestModel);
            return StatusCode((int)result.StatusCode, result);
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

    }
}
