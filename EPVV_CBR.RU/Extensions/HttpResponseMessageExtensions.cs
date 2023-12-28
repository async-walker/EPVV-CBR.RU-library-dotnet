using EPVV_CBR_RU.Exceptions;
using System.Runtime.CompilerServices;

namespace EPVV_CBR_RU.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static async Task<T> DeserializeContentAsync<T>(
        this HttpResponseMessage httpResponse)
        {
            Stream? contentStream = null;

            if (httpResponse.Content is null)
            {
                throw new RequestException(
                    message: "Response doesn't contain any content",
                    httpStatusCode: httpResponse.StatusCode);
            }

            try
            {
                T? deserializedObject;

                try
                {
                    contentStream = await httpResponse.Content
                        .ReadAsStreamAsync()
                        .ConfigureAwait(continueOnCapturedContext: false);

                    deserializedObject = contentStream
                        .DeserializeJsonFromStream<T>();
                }
                catch (Exception exception)
                {
                    throw CreateRequestException(
                        httpResponse: httpResponse,
                        message: "Required properties not found in response",
                        exception: exception
                    );
                }

                if (deserializedObject is null)
                {
                    throw CreateRequestException(
                        httpResponse: httpResponse,
                        message: "Required properties not found in response"
                    );
                }

                return deserializedObject;
            }
            finally
            {
#if NET6_0_OR_GREATER
                if (contentStream is not null)
                {
                    await contentStream.DisposeAsync().ConfigureAwait(false);
                }
#else
            contentStream?.Dispose();
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static RequestException CreateRequestException(
            HttpResponseMessage httpResponse,
            string message,
            Exception? exception = default
        ) =>
            exception is null
                ? new(
                    message: message,
                    httpStatusCode: httpResponse.StatusCode
                )
                : new(
                    message: message,
                    httpStatusCode: httpResponse.StatusCode,
                    innerException: exception
                );
    }
}
