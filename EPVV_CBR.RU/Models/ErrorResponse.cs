namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация JSON-объекта ошибки REST-методов
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP статус класса 4xx согласно Hypertext Transfer Protocol (HTTP) Status Code Registry 
        /// </summary>
        public int HTTPStatus { get; set; } = default!;
        /// <summary>
        /// Внутренний код ошибки Портала. Служит клиенту для автоматизированной обработки ошибок
        /// </summary>
        public string ErrorCode { get; set; } = default!;
        /// <summary>
        /// Расшифровка ошибки. Служит для человеко-читаемой обработки ошибок
        /// </summary>
        public string ErrorMessage { get; set; } = default!;
        /// <summary>
        /// Объект с дополнительно информацией к ошибке, по-умолчанию пустой
        /// </summary>
        public object? MoreInfo { get; set; } = null;
    }
}
