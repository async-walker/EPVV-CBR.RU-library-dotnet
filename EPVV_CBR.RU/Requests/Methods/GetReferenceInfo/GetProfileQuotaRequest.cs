using EPVV_CBR_RU.Types;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    public class GetProfileQuotaRequest : RequestBase<QuotaInfo>
    {
        public GetProfileQuotaRequest() : base(HttpMethod.Get, "profile/quota")
        { }
    }
}
