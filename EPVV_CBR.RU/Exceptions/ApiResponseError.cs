using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EPVV_CBR_RU.Exceptions
{
    /// <summary>
    /// Репрезентация JSON-объекта ошибки REST-методов
    /// </summary>
    /// <remarks>
    /// Инициализация экземпляра <see cref="ApiResponseError"/>
    /// </remarks>
    /// <param name="httpStatus">HTTP статус класса 4xx согласно Hypertext Transfer Protocol (HTTP) Status Code Registry</param>
    /// <param name="code">Внутренний код ошибки Портала. Служит клиенту для автоматизированной обработки ошибок</param>
    /// <param name="message">Расшифровка ошибки. Служит для человеко-читаемой обработки ошибок</param>
    /// <param name="moreInfo">Объект с дополнительной информацией к ошибке, по-умолчанию пустой</param>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ApiResponseError(
        int httpStatus,
        string code,
        string message,
        object? moreInfo)
    {
        /// <summary>
        /// HTTP статус класса 4xx согласно Hypertext Transfer Protocol (HTTP) Status Code Registry 
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int HTTPStatus { get; private set; } = httpStatus;
        /// <summary>
        /// Внутренний код ошибки Портала. Служит клиенту для автоматизированной обработки ошибок
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Code { get; private set; } = code;
        /// <summary>
        /// Расшифровка ошибки. Служит для человеко-читаемой обработки ошибок
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Message { get; private set; } = message;
        /// <summary>
        /// Объект с дополнительной информацией к ошибке, по-умолчанию пустой
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? MoreInfo { get; private set; } = moreInfo;
    }
}
