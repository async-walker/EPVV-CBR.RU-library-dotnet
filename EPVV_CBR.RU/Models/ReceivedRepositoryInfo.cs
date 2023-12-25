using EPVV_CBR_RU.Enums;

namespace EPVV_CBR_RU.Models
{
    /// <summary>
    /// Репрезентация полученной информации о репозитории
    /// </summary>
    public class ReceivedRepositoryInfo : RepositoryInfo
    {
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
