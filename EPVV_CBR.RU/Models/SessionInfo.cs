using Newtonsoft.Json;

namespace EPVV_CBR.RU.Models
{
    public class SessionInfo
    {
        [JsonProperty(nameof(UploadUrl))]
        public string UploadUrl { get; set; }
        [JsonProperty(nameof(ExpirationDateTime))]
        public string ExpirationDateTime { get; set; }
        [JsonIgnore()]
        public ResponseMessageFile MessageFile { get; set; }
    }
}
