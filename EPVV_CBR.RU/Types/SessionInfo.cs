using Newtonsoft.Json;

namespace EPVV_CBR_RU.Types
{
    /// <summary>
    /// Репрезентация информации о сессии для загрузки файла
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// Путь для загрузки файла
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string UploadUrl { get; set; } = default!;
        /// <summary>
        /// Дата и время истечения сессии
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ExpirationDateTime { get; set; } = default!;
    }
}
