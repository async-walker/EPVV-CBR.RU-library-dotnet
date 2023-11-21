namespace EPVV_CBR.RU.Models
{
    public class MessageInfo
    {
        public string Id { get; set; }
        public string CorrelationId { get; set; }
        public string GroupId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
        public string TaskName { get; set; }
        public string RegNumber { get; set; }
        public int TotalSize { get; set; }
        public Sender Sender { get; set; }
        public List<MessageFileUploaded> Files { get; set; }
    }
}
