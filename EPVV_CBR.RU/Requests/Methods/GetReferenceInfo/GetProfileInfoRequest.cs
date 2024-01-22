using EPVV_CBR_RU.Types;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    public class GetProfileInfoRequest : RequestBase<ProfileInfo>
    {
        public GetProfileInfoRequest() : base(HttpMethod.Get, "profile")
        { }
    }
}
