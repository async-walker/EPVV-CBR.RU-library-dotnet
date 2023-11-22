using EPVV_CBR.RU.Enums;
using EPVV_CBR.RU.Models;
using System.Net.Http.Headers;

namespace EPVV_CBR.RU.Extensions
{
    internal static class HttpExtensions
    {
        public static async Task<string> ReadContent(this HttpResponseMessage responseMessage)
        {
            var message = await responseMessage.Content.ReadAsStringAsync();

            return message;
        }

        public static async Task<HttpResponseMessage> GetResponse(
            this HttpClient client, 
            string credentials, 
            string endpoint, 
            HttpMethod method, 
            HttpContent? content = null, 
            ContentType? contentType = null, 
            long? contentLength = null, 
            ContentRangeHeaderValue? contentRange = null)
        {
            var uri = $"{client.BaseAddress}/{endpoint}";

            using (var request = new HttpRequestMessage(method, uri))
            {
                //request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Authorization", $"Basic {credentials}");

                if (contentType != null)
                {
                    ContentTypeDescription.TryGetValue(contentType, out var valueContentType);

                    request.Content = content!;
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(valueContentType);

                    if (contentLength != null)
                        request.Content.Headers.ContentLength = contentLength;

                    if (contentRange != null)
                        request.Content.Headers.ContentRange = contentRange;
                }

                var response = await client.SendAsync(request);

                return response;
            }
        }

        public static async Task NotSuccesStatusCodeCatcher(this HttpResponseMessage responseMessage, string message)
        {
            var messageContent = await responseMessage.ReadContent();

            var error = messageContent.DeserializeFromJson<ErrorResponse>()
                    ?? throw new HttpRequestException(
                        $"Возникла внутренняя ошибка - {responseMessage.StatusCode} ({(int)responseMessage.StatusCode})");

            throw new HttpRequestException(
                $"{message}\n\n" +
                $"Код: {error.ErrorCode} ({error.HTTPStatus})\n" +
                $"Описание: {error.ErrorMessage}\n" +
                $"Дополнительно: {error.MoreInfo}");
        }

        private static readonly Dictionary<ContentType?, string> ContentTypeDescription = new()
        {
            { ContentType.ApplicationJson, "application/json" },
            { ContentType.ApplicationOctetStream, "application/octet-stream" },
        };
    }
}
