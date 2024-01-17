using EPVV_CBR_RU.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetMyProfileRequest : RequestBase<ProfileInformation>
    {
        public GetMyProfileRequest() : base(HttpMethod.Get, "profile")
        { }
    }
}
