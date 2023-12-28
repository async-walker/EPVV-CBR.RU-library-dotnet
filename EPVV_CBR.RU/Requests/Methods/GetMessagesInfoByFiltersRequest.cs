using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPVV_CBR_RU.Models;

namespace EPVV_CBR_RU.Requests.Methods
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetMessagesInfoByFiltersRequest : RequestBase<List<MessageInfo>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public GetMessagesInfoByFiltersRequest(QueryFilters? filters = null) 
            : base(HttpMethod.Get, GetEndpoint(filters))
        {

        }

        private static string GetEndpoint(QueryFilters? filters = null)
        {
            var endpoint = "messages";
            var filterString = QueryFilters.ExecuteParams(filters);

            return endpoint + filterString;
        }
    }
}
