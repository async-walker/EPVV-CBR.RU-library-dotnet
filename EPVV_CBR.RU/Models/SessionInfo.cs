using Newtonsoft.Json;

namespace EPVV_CBR.RU.Models
{
    /// <summary>
    /// Репрезентация информации о сессии для загрузки файла
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// Путь для загрузки файла
        /// </summary>
        [JsonProperty(nameof(UploadUrl))]
        public string UploadUrl { get; set; } = default!;
        /// <summary>
        /// Дата и время истечения сессии
        /// </summary>
        [JsonProperty(nameof(ExpirationDateTime))]
        public string ExpirationDateTime { get; set; } = default!;
        /// <summary>
        /// Размер файла
        /// </summary>
        [JsonIgnore()]
        public long FileSize { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        [JsonIgnore()]
        public string FileName { get; set; } = string.Empty;
    }
}
