using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ConfirmSendMessageRequest : RequestBase<string>
    {
        public ConfirmSendMessageRequest(HttpMethod method, string endpoint) 
            : base(method, endpoint)
        {
        }
    }
}
