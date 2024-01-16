using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Requests;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Реализация интерфейса взаимодествия с ЕПВВ
    /// </summary>
    public class EpvvClient : IEpvvClient
    {
        private readonly EpvvClientOptions _options;
        private readonly HttpClient _httpClient;

        /// <inheritdoc/>
        public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();

        /// <summary>
        /// Инициализация клиента ЕПВВ
        /// </summary>
        /// <param name="options">Настройки сервиса</param>
        /// <param name="httpClient">Экземпляр HttpClient</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EpvvClient(EpvvClientOptions options, HttpClient? httpClient = default)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.BaseAddress = new Uri(_options.BaseAddress);
        }

        /// <inheritdoc/>
        public virtual async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request, 
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var path = request.Path.TryRemoveBaseAddressForPath(_httpClient.BaseAddress!.AbsoluteUri);

            var url = $"{_options.BaseAddress}/{path}";

            using var requestMessage = 
                new HttpRequestMessage(
                    method: request.Method,
                    requestUri: url)
                {
                    Content = request.ToHttpContent()
                };

            using var responseMessage =
                await GetAndHandleResponse(requestMessage, cancellationToken);

            var apiResponse = await responseMessage
                .DeserializeContentAsync<TResponse>()
                .ConfigureAwait(false);

            return apiResponse;
        }

        /// <inheritdoc/>
        public Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task DownloadFileAsync(
            string path, 
            Stream destination, 
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destination);

            var fileUri = $"{_options.BaseAddress}/{path}";

            using var requestMessage = 
                new HttpRequestMessage(
                    method: HttpMethod.Get,
                    requestUri: fileUri);

            using var responseMessage = 
                await GetAndHandleResponse(requestMessage, cancellationToken);

            if (responseMessage.Content is null)
            {
                throw new RequestException(
                    message: "Ответ не содержит какой-либо контент",
                    responseMessage.StatusCode);
            }

            try
            {
                await responseMessage.Content.CopyToAsync(destination)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during file download",
                    responseMessage.StatusCode,
                    exception);
            }
        }

        private async Task<HttpResponseMessage> GetAndHandleResponse(
            HttpRequestMessage requestMessage, 
            CancellationToken cancellationToken)
        {
            requestMessage.Headers.TryAddWithoutValidation(
                name: "Authorization",
                value: $"Basic {_options.Credentials}");

            using HttpResponseMessage httpResponse =
                await _httpClient.GetResponseAsync(requestMessage, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var failedApiResponse = await httpResponse
                    .DeserializeContentAsync<ApiResponseError>()
                    .ConfigureAwait(false);

                throw ExceptionsParser.Parse(failedApiResponse);
            }

            return httpResponse;
        }
    }
}
