namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiRequestException : RequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int HTTPStatus { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// 
        /// </summary>
        public object? MoreInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ApiRequestException(string message)
            : base(message)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        public ApiRequestException(string message, int httpStatus)
            : base(message) =>
            HTTPStatus = httpStatus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(string message, int httpStatus, Exception innerException)
            : base(message, innerException) =>
            HTTPStatus = httpStatus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="code"></param>
        /// <param name="moreInfo"></param>
        public ApiRequestException(
            string message, 
            int httpStatus,
            string code,
            object? moreInfo)
            : base(message)
        {
            HTTPStatus = httpStatus;
            Code = code;
            MoreInfo = moreInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="code"></param>
        /// <param name="moreInfo"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(
            string message,
            int httpStatus,
            string code,
            object? moreInfo,
            Exception innerException)
            : base(message, innerException)
        {
            HTTPStatus = httpStatus;
            Code = code;
            MoreInfo = moreInfo;
        }
    }
}
