using EPVV_CBR.RU.Models;

namespace EPVV_CBR.RU.Exceptions
{
    /// <summary>
    /// Репрезентация ошибки взаимодействия с ЕПВВ
    /// </summary>
    public class EpvvException : Exception
    {
        /// <summary>
        /// Инициализация ошибки взаимодействия с ЕПВВ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorResponse"></param>
        public EpvvException(string message, ErrorResponse errorResponse) 
            : base(message) => ErrorResponse = errorResponse;

        /// <summary>
        /// Ошибка REST-метода
        /// </summary>
        public ErrorResponse ErrorResponse { get; } = default!;
    }
}
