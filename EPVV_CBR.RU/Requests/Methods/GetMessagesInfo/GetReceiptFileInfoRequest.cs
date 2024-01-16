using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetMessagesInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetReceiptFileInfoRequest : RequestBase<UploadedFile>
    {
        public GetReceiptFileInfoRequest(
            string messageId, 
            string receiptId, 
            string fileId) 
            : base(HttpMethod.Get, $"messages/{messageId}/receipts/{receiptId}/files/{fileId}")
        { }
    }
}
