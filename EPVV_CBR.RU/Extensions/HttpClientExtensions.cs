using System.Runtime.CompilerServices;

namespace EPVV_CBR_RU.Extensions
{
    internal static class HttpClientExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static async Task<HttpResponseMessage> GetResponseAsync(
            this HttpClient httpClient,
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
                    throw;

                throw new Exception(
                    message: "Время запроса вышло",
                    innerException: exception);
            }
            catch (Exception exception)
            {
                throw new Exception(
                    message: "Исключение возникло при выполнении запроса",
                    innerException: exception);
            }

            return httpResponse;
        }
    }
}
