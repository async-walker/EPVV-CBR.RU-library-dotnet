using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.SendMessages
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ConfirmSendMessageRequest : RequestBase<object?>
    {
        public ConfirmSendMessageRequest(string messageId)
            : base(HttpMethod.Post, $"messages/{messageId}")
        { }
    }
}
