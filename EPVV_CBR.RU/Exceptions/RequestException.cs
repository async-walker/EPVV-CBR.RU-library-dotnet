using System.Net;

namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// Репрезентация ошибки запроса
    /// </summary>
    public class RequestException : Exception
    {
        /// <summary>
        /// HTTP-статусный код полученного ответа
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="RequestException"/>
        /// </summary>
        /// <param name="message"></param>
        public RequestException(string message)
            : base(message)
        { }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="RequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RequestException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="RequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        public RequestException(string message, HttpStatusCode httpStatusCode)
            : base(message) =>
            HttpStatusCode = httpStatusCode;

        /// <summary>
        /// Инициализация нового экземпляра <see cref="RequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="innerException"></param>
        public RequestException(string message, HttpStatusCode httpStatusCode, Exception innerException)
            : base(message, innerException) =>
            HttpStatusCode = httpStatusCode;
    }
}
