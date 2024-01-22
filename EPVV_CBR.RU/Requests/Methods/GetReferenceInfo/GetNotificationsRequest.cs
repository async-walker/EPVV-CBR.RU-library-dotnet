using EPVV_CBR_RU.Types;

namespace EPVV_CBR_RU.Requests.Methods.GetReferenceInfo
{
    public class GetNotificationsRequest : RequestBase<List<NotificationInfo>>
    {
        public GetNotificationsRequest() : base(HttpMethod.Get, "notifications")
        { }
    }
}
