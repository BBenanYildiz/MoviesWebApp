using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Model
{
    public class ApiResponse
    {
        public const string SuccessMessage = "İşlem Başarılı";
        public const string ErrorMessage = "İşlem Başarısız";

        public ApiResponse()
        {

        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess
        {
            get
            {
                return (int)StatusCode >= 200 && (int)StatusCode <= 299;
            }
        }

        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static ApiResponse CreateResponse(HttpStatusCode httpStatusCode, string message, object data) => new(httpStatusCode, message, data);

        public static ApiResponse CreateResponse(HttpStatusCode httpStatusCode, string message) => new(httpStatusCode, message, null);
    }
}
