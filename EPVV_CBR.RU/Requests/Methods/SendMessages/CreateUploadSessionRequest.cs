using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.SendMessages
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CreateUploadSessionRequest : RequestBase<SessionInfo>
    {
        public CreateUploadSessionRequest(string messageId, string fileId)
            : base(
                  method: HttpMethod.Post,
                  endpoint: $"messages/{messageId}/files/{fileId}/createUploadSession")
        { }
    }
}
