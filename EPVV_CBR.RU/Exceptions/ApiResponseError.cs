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
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ApiResponseError
    {
        /// <summary>
        /// HTTP статус класса 4xx согласно Hypertext Transfer Protocol (HTTP) Status Code Registry 
        /// </summary>
        [JsonProperty(nameof(HTTPStatus), Required = Required.Always)]
        public int HTTPStatus { get; private set; }
        /// <summary>
        /// Внутренний код ошибки Портала. Служит клиенту для автоматизированной обработки ошибок
        /// </summary>
        [JsonProperty("ErrorCode", Required = Required.Always)]
        public string Code { get; private set; } = default!;
        /// <summary>
        /// Расшифровка ошибки. Служит для человеко-читаемой обработки ошибок
        /// </summary>
        [JsonProperty("ErrorMessage", Required = Required.Always)]
        public string Message { get; private set; } = default!;
        /// <summary>
        /// Объект с дополнительной информацией к ошибке, по-умолчанию пустой
        /// </summary>
        [JsonProperty(nameof(MoreInfo), DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? MoreInfo { get; private set; }
    }
}
