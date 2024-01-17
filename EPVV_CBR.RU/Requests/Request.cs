using EPVV_CBR_RU.Requests.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EPVV_CBR_RU.Requests
{
    /// <summary>
    /// Репрезентация запроса к API
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class Request : IRequest
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public HttpMethod Method { get; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string Path { get; }

        /// <summary>
        /// Инициализация запроса
        /// </summary>
        /// <param name="method">HTTP-метод</param>
        /// <param name="path">Путь к методу API</param>
        protected Request(HttpMethod method, string path)
        {
            Method = method;
            Path = path;
        }

        public virtual HttpContent? ToHttpContent() =>
            new StringContent(
                content: JsonConvert.SerializeObject(this),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
    }
}
