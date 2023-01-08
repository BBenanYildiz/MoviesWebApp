using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Movies.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {
            string apiKey = "2ffe153e25788cfac01580dae1018af4";
            string apiUrl = "https://api.themoviedb.org/3/movie/550?api_key=" + apiKey;

            var data = HttpHelper.GetDataFromApi<List<Movie>>(apiUrl).Result;


            HttpWebRequest request;
            Stream RequestStream;
            StreamWriter RequestWriter;
            HttpWebResponse HttpResponse;
            Stream ResponseStream;
            StreamReader ResponseReader;

            int timeout = 36000;

            request = (HttpWebRequest)HttpWebRequest.Create("https://api.themoviedb.org/3/movie/550?api_key=" + apiKey);

            try
            {
                RequestStream = request.GetRequestStream();
                Request.Method = "GET";

                RequestStream = request.GetRequestStream();
                RequestWriter = new StreamWriter(RequestStream, Encoding.ASCII);
                RequestWriter.Write(false);
                RequestWriter.Close();

                //request = (HttpWebRequest)request.GetResponse();
                //Response
            }
            catch (Exception ex)
            {

                throw;
            }


            return Ok(data);
           
        }

       
        public class HttpHelper
        {
            public static async Task<T> GetDataFromApi<T>(string url)
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                    return resultContent;
                }
            }
        }

        
    }
}
