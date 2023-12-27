using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Requests.Abstractions;

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

            var url = $"{_options.BaseAddress}/{request.Endpoint}";

            using var requestMessage = new HttpRequestMessage(
                method: request.Method,
                requestUri: url)
            {
                Content = request.ToHttpContent()
            };

            requestMessage.Headers.TryAddWithoutValidation(
                name: "Authorization",
                value: $"Basic {_options.Credentials}");

            using var responseMessage = await SendRequestAsync(
                _httpClient, 
                requestMessage, 
                cancellationToken)
                .ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
            {
                //throw ExceptionsParser.Parse(new ApiResponseError());
            }

            var apiResponse = await responseMessage
                .DeserializeContentAsync<TResponse>()
                .ConfigureAwait(false);

            return apiResponse;

            static async Task<HttpResponseMessage> SendRequestAsync(
                HttpClient httpClient,
                HttpRequestMessage requestMessage,
                CancellationToken cancellationToken)
            {
                HttpResponseMessage? httpResponse;
                
                try
                {
                    httpResponse = await httpClient
                        .SendAsync(requestMessage, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (TaskCanceledException exception)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }

                    throw new Exception(message: "Request timed out", innerException: exception);
                }
                catch (Exception exception)
                {
                    throw new Exception(
                        message: "Exception during making request",
                        innerException: exception
                    );
                }

                return httpResponse;
            }
        }

        /// <inheritdoc/>
        public Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DownloadFileAsync(string filePath, Stream destination, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
