using EPVV_CBR_RU.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UploadFileRequest : FileRequestBase<UploadedFile>
    {
        public UploadFileRequest(SessionInfo sessionInfo, string filePath) 
            : base(HttpMethod.Put, sessionInfo.UploadUrl)
        {
            ByteData = File.ReadAllBytes(filePath);
        }

        /// <inheritdoc />
        public override HttpContent? ToHttpContent() =>
             ToByteArrayContent(ByteData);

        //{
        //    using (var content = new MultipartFormDataContent())
        //    {
        //        var contentLength = 12;
        //        var contentRange = new ContentRangeHeaderValue(0, contentLength - 1, contentLength);

        //        var httpClient = new HttpClient();

        //        var httpRequest = new HttpRequestMessage(HttpMethod.Put, "");

        //        httpRequest.Content = content;

        //        var response = httpClient.Send(httpRequest);

        //        var message = response.Content.ReadAsStream();

        //        var m = message.ToString();
        //    }
        //}
    }
}
