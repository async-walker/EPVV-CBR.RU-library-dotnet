using System.Net;

namespace EPVV_CBR.RU.Models
{
    public record ResponseViewModel<TModel> where TModel : class
    {
        public TModel? ResponseModel { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public ResponseViewModel(TModel? responseModel, HttpStatusCode statusCode, string? message = null)
        {
            ResponseModel = responseModel;
            StatusCode = statusCode;
            Message = message;
        }
    }

    public record ResponseViewModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public ResponseViewModel(HttpStatusCode statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
