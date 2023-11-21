using Newtonsoft.Json;

namespace EPVV_CBR.RU.Models
{
    public class MessageSession
    {
        [JsonProperty("MsgId")]
        public string MessageId { get; set; }
        [JsonProperty("FileId")]
        public string FileId { get; set; }

        public MessageSession(string messageId, string fileId)
        {
            MessageId = messageId;
            FileId = fileId;
        }
    }
}
