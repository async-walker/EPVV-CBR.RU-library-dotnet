using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.DeleteMessages
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class DeleteMessageRequest : Request
    {
        public DeleteMessageRequest(string messageId) 
            : base(HttpMethod.Delete, $"messages/{messageId}")
        { }
    }
}
