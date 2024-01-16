using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetMessagesInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetReceiptByIdRequest : RequestBase<Receipt>
    {
        public GetReceiptByIdRequest(string messageId, string receiptId) 
            : base(HttpMethod.Get, $"messages/{messageId}/receipts/{receiptId}")
        { }
    }
}
