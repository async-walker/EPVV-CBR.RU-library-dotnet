using EPVV_CBR.RU.Data.Enums;

namespace EPVV_CBR.RU.Models
{
    public class MessageFile
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public FileType FileType { get; set; }
        public bool Encrypted { get; set; }
        public string? SignedFile { get; set; }
        public long Size { get; set; }
        public string? RepositoryType { get; set; }
        public UploadRepository? RepositoryInfo { get; set; }
    }
}
