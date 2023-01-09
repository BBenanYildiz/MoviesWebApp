using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Service.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : CustomBaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MoviesController(IConfiguration configuration,
            IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm filmleri getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            //Şimdilik burda kalsın burayıda düzenlicez.
            string apiKey = ApiKey();
            string baseUrl = BaseUrl();
             string apiUrl = baseUrl + "movie/popular?api_key=" + apiKey;

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
            //Filmin Detayını Döndüğümüz Kısım Api

            string apiKey = ApiKey();
            string baseUrl = BaseUrl();

            string apiUrl = baseUrl + "movie/"+ id + "?api_key=" + apiKey;
            var response = WebHelper.Get(apiUrl);
            var myDeserializedClass = JsonConvert.DeserializeObject<Movie>(response);

            var movieDtos = _mapper.Map <MovieDTOs>(myDeserializedClass);

            return CreateActionResult(CustomResponseDto<MovieDTOs>.Success(200, movieDtos));
        }

        /// <summary>
        /// Seçilen filme not ve puan ekleme yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post(int id)
        {
            //Seçilen Filme Not Ve Puan Ekleme 

            return Ok();
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
            //Filmi Tavsiye Etme 

            var result = MoviesService.SharedMail(id, mailAdress);

            //Sonuça göre işlem yapıcaz direk resultu basıcaz

            if (!string.IsNullOrEmpty(result))
            {
                // Bakılacak
                return CreateActionResult(CustomResponseDto<NoContentDTO>.Success(200));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<NoContentDTO>.Fail(400, "E-Posta Gönderilirken Bir Hata İle Karşılaşıldı."));
            }
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
