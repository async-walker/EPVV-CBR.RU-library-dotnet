using System.Runtime.CompilerServices;

namespace EPVV_CBR_RU.Extensions
{
    internal static class HttpContentExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static MultipartFormDataContent AddContent(
            this MultipartFormDataContent multipartContent,
            Stream stream)
        {
            var mediaPartContent = new StreamContent(stream)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Length", $"{stream.Length}" },
                    {"Content-Range", $"bytes 0-{stream.Length-1}/{stream.Length}" }
                },
            };

            multipartContent.Add(mediaPartContent);

            return multipartContent;
        }
    }
}
