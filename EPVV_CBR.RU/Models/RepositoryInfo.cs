namespace EPVV_CBR.RU.Models
{
    public class ResponseRepositoryInfo : RepositoryInfo
    {
        public string CheckSum { get; set; }
        public string CheckSumType { get; set; }
    }

    public class RepositoryInfo
    {
        public string Path { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string RepositoryType { get; set; }
    }
}
