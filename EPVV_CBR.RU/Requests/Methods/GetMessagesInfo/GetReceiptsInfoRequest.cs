using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetMessagesInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetReceiptsInfoRequest : RequestBase<List<Receipt>>
    {
        public GetReceiptsInfoRequest(string messageId) 
            : base(HttpMethod.Get, $"messages/{messageId}/receipts")
        { }
    }
}
