using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Requests.Abstractions;
using System.Runtime.CompilerServices;

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

            var endpoint = request.Endpoint.TryRemoveBaseAddressForEndpoint(_httpClient.BaseAddress!.AbsoluteUri);

            var url = $"{_options.BaseAddress}/{endpoint}";

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
                var failedApiResponse = await responseMessage
                    .DeserializeContentAsync<ApiResponseError>()
                    .ConfigureAwait(false);

                throw ExceptionsParser.Parse(failedApiResponse);
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
        public async Task DownloadFileAsync(
            string endpoint, 
            Stream destination, 
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destination);

            var fileUri = $"{_options.BaseAddress}/{endpoint}";

            using var requestMessage = new HttpRequestMessage(
                method: HttpMethod.Get,
                requestUri: fileUri);

            requestMessage.Headers.TryAddWithoutValidation(
                name: "Authorization",
                value: $"Basic {_options.Credentials}");

            using HttpResponseMessage httpResponse = await GetResponseAsync(
                _httpClient,
                requestMessage,
                cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var failedApiResponse = await httpResponse
                    .DeserializeContentAsync<ApiResponseError>()
                    .ConfigureAwait(false);

                throw ExceptionsParser.Parse(failedApiResponse);
            }

            if (httpResponse.Content is null)
            {
                throw new RequestException(
                    message: "Response doesn't contain any content",
                    httpResponse.StatusCode);
            }

            try
            {
                await httpResponse.Content.CopyToAsync(destination)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during file download",
                    httpResponse.StatusCode,
                    exception);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> GetResponseAsync(
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
                if (cancellationToken.IsCancellationRequested) { throw; }

                throw new RequestException(
                    message: "Request timed out",
                    innerException: exception
                );
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during file download",
                    innerException: exception
                );
            }

            return httpResponse;
        }
    }
}
