namespace EPVV_CBR.RU.Models
{
    public class Receipt
    {
        public string ReceiveTime { get; set; }
        public string StatusTime { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ResponseMessageFile> Files { get; set; }
    }
}
