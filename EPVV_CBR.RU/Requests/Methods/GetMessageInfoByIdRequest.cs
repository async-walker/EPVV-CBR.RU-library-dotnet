using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPVV_CBR_RU.Models;

namespace EPVV_CBR_RU.Requests.Methods
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetMessageInfoByIdRequest : RequestBase<MessageInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        public GetMessageInfoByIdRequest(string messageId) 
            : base(HttpMethod.Get, $"messages/{messageId}")
        {

        }
    }
}
