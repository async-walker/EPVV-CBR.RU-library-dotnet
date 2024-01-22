using EPVV_CBR_RU.Types;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    public class GetGuideListRequest : RequestBase<List<GuideInfo>>
    {
        public GetGuideListRequest() : base(HttpMethod.Get, "dictionaries")
        { }
    }
}
