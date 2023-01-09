using Microsoft.AspNetCore.Mvc;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Service.Services;
using Newtonsoft.Json;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MoviesController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            string apiUrl = "https://api.themoviedb.org/3/movie/popular?api_key=" + "2ffe153e25788cfac01580dae1018af4";

            var response = WebHelper.Get(apiUrl);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);

            return Ok(myDeserializedClass);
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
            //Filmin Detayını Döndüğümüz Kısı Api

            return Ok();
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

            return Ok();
        }


        //AppSetting Dosyasından Api Keyi Çekiyoruz.
        private string ApiKey()
        {
            var apiKey = _configuration.GetValue<string>("TmbdApiKey:api_key");
            return apiKey;
        }
    }
}
