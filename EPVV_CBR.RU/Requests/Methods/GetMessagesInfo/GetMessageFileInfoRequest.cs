using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetMessagesInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetMessageFileInfoRequest : RequestBase<UploadedFile>
    {
        public GetMessageFileInfoRequest(string messageId, string fileId) 
            : base(HttpMethod.Get, $"messages/{messageId}/files/{fileId}")
        { }
    }
}
