namespace EPVV_CBR.RU.Models
{
    public class ResponseMessageBody
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
        public long TotalSize { get; set; }
        public Sender Sender { get; set; }
        public List<ResponseMessageFile> Files { get; set; }
        public List<Receipt> Receipts { get; set; }
    }

    public class ResponseMessageFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string FileType { get; set; }
        public bool Encrypted { get; set; }
        public string? SignedFile { get; set; }
        public long Size { get; set; }
        public List<ResponseRepositoryInfo> RepositoryInfo { get; set; }
    }
}
