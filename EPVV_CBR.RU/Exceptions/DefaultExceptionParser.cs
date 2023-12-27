namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// Дефолтная реализация <see cref="IExceptionParser"/>, всегда возвращает <see cref="ApiRequestException"/>
    /// </summary>
    public class DefaultExceptionParser : IExceptionParser
    {
        /// <inheritdoc />
        public ApiRequestException Parse(ApiResponseError apiResponse)
        {
            if (apiResponse is null)
            {
                throw new ArgumentNullException(nameof(apiResponse));
            }

            return new(
                message: apiResponse.Message,
                httpStatus: apiResponse.HTTPStatus,
                code: apiResponse.Code,
                moreInfo: apiResponse.MoreInfo
            );
        }
    }
}
