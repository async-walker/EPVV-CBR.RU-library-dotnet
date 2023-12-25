namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// Репрезентация ошибки взаимодействия с ЕПВВ
    /// </summary>
    public class EPVVException : Exception
    {
        /// <summary>
        /// Инициализация ошибки взаимодействия с ЕПВВ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public EPVVException(string message, PortalError error) 
            : base(message) => Error = error;

        /// <summary>
        /// Ошибка REST-метода
        /// </summary>
        public PortalError Error { get; } = default!;
    }
}
