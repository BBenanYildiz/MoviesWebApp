using System.Net;

namespace Movies.Core.Helper
{
    public class WebHelper
    {
        /// <summary>
        /// Gönderilen URL istek atar ve deserialize edilmeden responsu geri gönderir
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// 
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string ApiKey()
        {
            return "2ffe153e25788cfac01580dae1018af4";
        }

        public static string BaseUrl()
        {
            return "https://api.themoviedb.org/3/";
        }

        public static string CreateUrlApiKey(string url)
        {
            return BaseUrl() + url + ApiKey();
        } 
    }
}
