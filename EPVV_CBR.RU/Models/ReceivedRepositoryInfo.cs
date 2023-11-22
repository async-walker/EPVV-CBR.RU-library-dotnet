using EPVV_CBR.RU.Data.Enums;
using System.Text.Json.Serialization;

namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация полученной информации о репозитории
    /// </summary>
    public class ReceivedRepositoryInfo : RepositoryInfo
    {
        [JsonConstructor]
        private ReceivedRepositoryInfo(
            string path, 
            string host,
            string checkSum,
            string checkSumType,
            RepositoryType repositoryType) : base(checkSum, checkSumType, path)
        {
            Host = host;
            RepositoryType = repositoryType;
        }

        /// <summary>
        /// IP адрес или имя узла репозитория
        /// </summary>
        public string Host { get; set; } = default!;
        /// <summary>
        /// Порт для обращения к репозиторию
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Тип репозитория
        /// </summary>
        public RepositoryType RepositoryType { get; set; }
    }
}
