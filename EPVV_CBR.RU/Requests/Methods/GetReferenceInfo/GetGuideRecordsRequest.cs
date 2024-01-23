using EPVV_CBR_RU.Types;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    public class GetGuideRecordsRequest : RequestBase<GuideRecordsInfo>
    {
        public GetGuideRecordsRequest(string guideId, int? page = default)
            : base(HttpMethod.Get, GetPathByPage(guideId, page))
        { }

        private static string GetPathByPage(string guideId, int? page = default)
        {
            var path = $"dictionaries/{guideId}";

            if (page is not null)
                path += $"?page={page}";

            return path;
        }
    }
}