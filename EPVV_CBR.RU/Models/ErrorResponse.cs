namespace EPVV_CBR.RU.Models
{
    public class ErrorResponse
    {
        public int HTTPStatus { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object MoreInfo { get; set; } = null;
    }
}
