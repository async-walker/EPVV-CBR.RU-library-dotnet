namespace EPVV_CBR.RU.Models
{
    public class UploadedFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Encrypted { get; set; }
        public string SignedFile { get; set; }
        public long Size { get; set; }
        public List<RepositoryInfo> RepositoryInfo { get; set; }
    }
}
