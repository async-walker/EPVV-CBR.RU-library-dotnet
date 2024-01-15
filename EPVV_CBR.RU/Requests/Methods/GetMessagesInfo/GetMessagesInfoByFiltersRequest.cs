using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Responses;

namespace EPVV_CBR_RU.Requests.Methods.GetMessagesInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetMessagesInfoByFiltersRequest : RequestBase<List<MessageInfo>>
    {
        public GetMessagesInfoByFiltersRequest(QueryFilters? filters = null)
            : base(HttpMethod.Get, GetEndpoint(filters))
        { }

        private static string GetEndpoint(QueryFilters? filters = null)
        {
            var endpoint = "messages";
            var filterString = QueryFilters.ExecuteFilters(filters);

            return endpoint + filterString;
        }
    }
}
