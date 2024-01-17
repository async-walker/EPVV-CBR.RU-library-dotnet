using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.DeleteMessages
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]

    public class DeleteFileOrSessionRequest : Request
    {
        public DeleteFileOrSessionRequest(string messageId, string fileId) 
            : base(HttpMethod.Delete, $"messages/{messageId}/files/{fileId}")
        { }
    }
}
