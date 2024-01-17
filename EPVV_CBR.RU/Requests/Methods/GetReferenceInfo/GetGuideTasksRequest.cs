using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetGuideTasksRequest : RequestBase<List<GuideTask>>
    {
        public GetGuideTasksRequest(DirectionExchangeType? directionExchange = default) 
            : base(HttpMethod.Get, QueryGenerator(directionExchange))
        { }

        static string QueryGenerator(DirectionExchangeType? directionExchange)
        {
            var path = "tasks";

            if (directionExchange is not null)
                path += $"?direction={(int)directionExchange}";

            return path;
        }
    }
}
