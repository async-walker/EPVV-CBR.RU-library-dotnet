namespace EPVV_CBR_RU.Requests
{
    public abstract class FileRequestBase<TResponse> : RequestBase<TResponse>
    {
        protected FileRequestBase(HttpMethod method, string endpoint)
            : base(method, endpoint)
        { }

        public byte[] ByteData { get; set; }

        /// <summary>
        /// Generate multipart form data content
        /// </summary>
        /// <param name="fileParameterName"></param>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        protected ByteArrayContent ToByteArrayContent(
            byte[] byteData)
        {
            if (byteData is null or { Length: 0 })
                throw new ArgumentNullException("Byte data or it's content is null");

            return GenerateByteArrayContent(byteData);
        }

        /// <summary>
        /// Generate multipart form data content
        /// </summary>
        /// <param name="exceptPropertyNames"></param>
        /// <returns></returns>
        protected ByteArrayContent GenerateByteArrayContent(byte[] byteData)
        {
            var mediaPartContent = new ByteArrayContent(byteData)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Length", $"{ByteData.Length}" },
                    {"Content-Range", $"bytes 0-{ByteData.Length-1}/{ByteData.Length}" }
                },
            };

            return mediaPartContent;
        }
    }
}
