namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о квоте профиля (десериализуется)
    /// </summary>
    public class QuotaInfo
    {
        /// <summary>
        /// Информация о доступной квоте в байтах
        /// </summary>
        public long TotalQuota { get; set; }
        /// <summary>
        /// Информация об использованной квоте в байтах
        /// </summary>
        public long UsedQuota { get; set; }
        /// <summary>
        /// Информация о максимальном размере сообщения в байтах
        /// </summary>
        public long MessageSize { get; set; }
    }
}
