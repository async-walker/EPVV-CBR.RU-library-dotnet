namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// Реализация <see cref="IExceptionParser"/> по умолчанию. Всегда возвращает <see cref="ApiRequestException"/>
    /// </summary>
    public class DefaultExceptionParser : IExceptionParser
    {
        /// <inheritdoc />
        public ApiRequestException Parse(ApiResponseError apiResponse)
        {
            return apiResponse is null
                ? throw new ArgumentNullException(nameof(apiResponse))
                : new(
                message: apiResponse.Message,
                httpStatus: apiResponse.HTTPStatus,
                code: apiResponse.Code,
                moreInfo: apiResponse.MoreInfo
            );
        }
    }
}
